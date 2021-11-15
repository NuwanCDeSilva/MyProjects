using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;

namespace FF.DataAccessLayer
{
    public class SCMCommonDAL : Inventory
    {
        public DataTable GetItemBalanceAvg(string _company, string _category, string _item, string _status)
        {
            //sp_getavgcostforbalance(p_com in NVARCHAR2,p_catcode in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getavgcostforbalance", CommandType.StoredProcedure, false, param);
            return _tblData;
        }

        //kapila 24/8/2012
        public DataTable GetRCCSerialSearchData(string _com, string _loc, int _isSameLoc, int _isStockItem, string _searchText, string _searchCriteria)
        {
            DataTable _dtResults = null;
            string _warranty = null;
            string _item = null;
            string _serial = null;
            string _docNo = null;
            string _invNo = null;
            string _accNo = null;

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

                    case "INVOICE NO":
                        _invNo = _searchText;
                        break;

                    case "ACC NO":
                        _accNo = _searchText;
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
            _invNo = (_invNo != null) ? (_invNo.ToUpper() + "%") : null;
            _accNo = (_accNo != null) ? (_accNo.ToUpper() + "%") : null;

            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[5] = new OracleParameter("p_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invNo;
            (param[6] = new OracleParameter("p_accno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _accNo;
            (param[7] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[8] = new OracleParameter("p_is_same_loc", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isSameLoc;
            (param[9] = new OracleParameter("p_is_stock_item", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isStockItem;
            param[10] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_rcc_srch_serial", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public Int32 UpdateAvgCost(string _company, string _location, string _category, string _item, string _status, string _user, decimal _avgcost)
        {
            Int32 _effect = 0;
            //sp_updateavgcost(p_com in NVARCHAR2,p_location in NVARCHAR2,p_catcode in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2,p_avgcost in NUMBER,p_user in NVARCHAR2,o_effect out NUMBER)

            OracleParameter[] param = new OracleParameter[8];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[5] = new OracleParameter("p_avgcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _avgcost;
            (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_updateavgcost", CommandType.StoredProcedure, param);

            return _effect;
        }

        public List<InventoryLocation> GetSCMInventoryBalance(string _company, string _location, string _item)
        {
            //sp_getinvoicetorybalance(p_com in NVARCHAR2,p_loc in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_getinvoicetorybalance", CommandType.StoredProcedure, false, param);
            return ConvertInventoryBalance(_tblData);
        }

        private List<InventoryLocation> ConvertInventoryBalance(DataTable _tbl)
        {
            List<InventoryLocation> _lst = new List<InventoryLocation>();
            if (_tbl != null)
                if (_tbl.Rows.Count > 0)
                {
                    foreach (DataRow _r in _tbl.Rows)
                    {
                        InventoryLocation _one = new InventoryLocation();
                        _one.Inl_free_qty = Convert.ToDecimal(_r["free_qty"]);
                        _one.Inl_itm_stus = Convert.ToString(_r["status"]);
                        _one.Inl_qty = Convert.ToDecimal(_r["qty_in_hand"]);
                        _one.Inl_res_qty = Convert.ToDecimal(_r["reserved_qty"]);
                        _one.Inl_itm_cd = Convert.ToString(_r["item_code"]);
                        _lst.Add(_one);
                    }
                }
            return _lst;
        }

        public DataTable GetInventoryTrackerSearchData(string _initialSearchParams)
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

            //Set relavant parameters according to the,search catergory.

            //Modify parameter values for LIKE search.
            //   searchParams[0] = (searchParams[0] != "" || searchParams[0] != null) ? (searchParams[0].ToUpper() + "%") : "%";
            //   searchParams[1] = (searchParams[1] != "" || searchParams[1] != null) ? (searchParams[1].ToUpper() + "%") : "%";
            //   searchParams[2] = (searchParams[2] != "" || searchParams[2] != null) ? (searchParams[2].ToUpper() + "%") : "%";
            ////   searchParams[3] = (searchParams[3] != "" || searchParams[3] != null) ? (searchParams[3].ToUpper() + "%") : "%";
            //   searchParams[4] = (searchParams[4] != "" || searchParams[4] != null) ? (searchParams[4].ToUpper() + "%") : "%";
            //   searchParams[5] = (searchParams[5] != "" || searchParams[5] != null) ? (searchParams[5].ToUpper() + "%") : "%";
            //   searchParams[6] = (searchParams[6] != "" || searchParams[6] != null) ? (searchParams[6].ToUpper() + "%") : "%";
            //   searchParams[7] = (searchParams[7] != "" || searchParams[7] != null) ? (searchParams[7].ToUpper() + "%") : "%";
            //   searchParams[8] = (searchParams[8] != "" || searchParams[8] != null) ? (searchParams[8].ToUpper() + "%") : "%";
            //   searchParams[9] = (searchParams[9] != "" || searchParams[9] != null) ? (searchParams[9].ToUpper() + "%") : "%";
            //   searchParams[10] = (searchParams[10] != "" || searchParams[10] != null) ? (searchParams[10].ToUpper() + "%") : "%";
            //   searchParams[11] = (searchParams[11] != "" || searchParams[11] != null) ? (searchParams[11]) : "%";
            //   searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : "%";
            //   searchParams[13] = (searchParams[13] != "" || searchParams[13] != null) ? (searchParams[13].ToUpper() + "%") : "%";
            searchParams[0] = (searchParams[0] != "" || searchParams[0] != null) ? (searchParams[0].ToUpper() + "%") : "%";
            searchParams[1] = (searchParams[1] != "" || searchParams[1] != null) ? (searchParams[1].ToUpper() + "%") : "%";
            searchParams[2] = (searchParams[2] != "" || searchParams[2] != null) ? (searchParams[2].ToUpper() + "%") : "%";
            //  searchParams[3] = (searchParams[3] != "" || searchParams[3] != null) ? (searchParams[3].ToUpper() + "%") : "%";
            searchParams[4] = (searchParams[4] != "" || searchParams[4] != null) ? (searchParams[4].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[5] = (searchParams[5] != "" || searchParams[5] != null) ? (searchParams[5].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[6] = (searchParams[6] != "" || searchParams[6] != null) ? (searchParams[6].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[7] = (searchParams[7] != "" || searchParams[7] != null) ? (searchParams[7].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[8] = (searchParams[8] != "" || searchParams[8] != null) ? (searchParams[8].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[9] = (searchParams[9] != "" || searchParams[9] != null) ? (searchParams[9].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[10] = (searchParams[10] != "" || searchParams[10] != null) ? (searchParams[10].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[11] = (searchParams[11] != "" || searchParams[11] != null) ? (searchParams[11]) : "%";
            //  searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : "%";
            searchParams[13] = (searchParams[13] != "" || searchParams[13] != null) ? (searchParams[13].ToUpper() + "%") : "%";

            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("P_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0];
            (param[1] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1];
            (param[2] = new OracleParameter("p_ststus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2];
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[3];
            (param[4] = new OracleParameter("p_main", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[4];
            (param[5] = new OracleParameter("p_sub", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[5];
            (param[6] = new OracleParameter("p_cha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[6];
            (param[7] = new OracleParameter("p_subcha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[7];
            (param[8] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[8];
            (param[9] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[9];
            (param[10] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[10];
            (param[11] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[11];
            (param[12] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[12];
            (param[13] = new OracleParameter("p_sub1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[13];
            param[14] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblZIT", "sp_searchinventoryitem", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetSCMSerialiItem(string _serialtype, string _company, string _serial, Int16 _isWholeWord)
        {
            //sp_getserialitem(p_serialtype in NVARCHAR2,  p_com in VARCHAR2, p_serial in NVARCHAR2,p_wholeword in NUMBER,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_serialtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialtype;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[3] = new OracleParameter("p_wholeword", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isWholeWord;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_getserialitem", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMCurrentLocation(string _serialType, string _company, string _serial, string _item)
        {
            //sp_getseriallocation(p_com in NVARCHAR2,p_item in NVARCHAR2,p_serial in NVARCHAR2,p_serialtype in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[3] = new OracleParameter("p_serialtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialType;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_getseriallocation", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMMovement(string _serialType, string _company, string _serial, string _item)
        {
            // sp_getMovement(p_com in NVARCHAR2, p_serial in NVARCHAR2,p_item in NVARCHAR2,p_serialtype in NVARCHAR2,c_data out sys_refcursor )
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_serialtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialType;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_getMovement", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMInvoice(string _company, string _customer, string _item)
        {
            //sp_getcustomerinvoice(p_com in NVARCHAR2,p_customer in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getcustomerinvoice", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMInvoiceSerial(string _company, string _invoice, string _item)
        {
            //sp_getinvoiceserial(p_com in NVARCHAR2,p_invoice in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getinvoiceserial", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMInvoiceDetail(string _company, string _invoice, string _item)
        {
            //sp_getinvoicedetail(p_com in NVARCHAR2,p_invoice in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getinvoicedetail", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMWarranty(string _item, string _serial, string _invoice)
        {
            //sp_getwarranty(p_item in NVARCHAR2,p_serial in NVARCHAR2,p_invoice in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[2] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getwarranty", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetInvoiceDetail(string _invoiceno, string _item, int _lineno, decimal _qty)
        {
            //sp_getinvoiceitemdetail(p_invoice in NVARCHAR2,p_item in NVARCHAR2,p_lineno in NUMBER,p_qty in NUMBER,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceno;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineno;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getinvoiceitemdetail", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetWarrantyClaimAdj(string _adjustment)
        {
            //sp_getwaracalimadj(p_doc in NVARCHAR2,c_data out sys_refcursor)

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _adjustment;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_getwaracalimadj", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public int UpdateWarrantyClaimInvoice(string _item, Int32 _lineno, string _invoiceno, decimal _qty)
        {
            //sp_updatewarrantyclaiminvoice(p_item in NVARCHAR2,p_lineno in NUMBER,p_invoice in NVARCHAR2,p_qty in NUMBER,o_effect out NUMBER)
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("p_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineno;
            (param[2] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceno;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_updatewarrantyclaiminvoice", CommandType.StoredProcedure, param);

            return effects;
        }

        //Chamal 02/04/2013
        public DataTable CheckIsAodReceived(string _document)
        {
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_aodno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tbl", "sp_checkisaodreceived", CommandType.StoredProcedure, false, param);
            return _list;
        }

        public int SaveServiceJobHeader(SCMServiceJobHeader _header)
        {
            string sql = " insert into ser_job_header " +
             " (sjb_jobno, sjb_date, sjb_location, sjb_company, " +
             " sjb_channel, sjb_jobcategory, sjb_jobtype, sjb_jobsubtype," +
             " sjb_manualref, sjb_otherref, sjb_reqno, sjb_jobstage," +
             " sjb_remarks, sjb_prority, sjb_startdate, sjb_enddate," +
             " sjb_noofprint, sjb_lastprintby, sjb_orderno, sjb_mainreqno," +
             " sjb_mainjobno, sjb_mainreqlocation, sjb_custcode," +
             " sjb_custtitle, sjb_custname, sjb_idtype, sjb_idno," +
             " sjb_address1, sjb_address2, sjb_town, sjb_phoneno," +
             " sjb_faxno, sjb_mobino, sjb_cont_person, sjb_cont_address1," +
             " sjb_cont_address2, sjb_cont_phone_no, sjb_job_remarks," +
             " sjb_tech_remarks, sjb_bill_cus_code, sjb_bill_cus_name," +
             " sjb_bill_address1, sjb_bill_address2, sjb_bill_address3," +
             " sjb_bill_phone_no, sjb_aodissueloca, sjb_aodissuedate," +
             " sjb_aodissueno, sjb_aodrecno, sjb_status, sjb_createby," +
             " sjb_createwhen, sjb_lastmodifyby, sjb_lastmodifywhen,sjb_profitcenter, " +
             " sjb_insucompany,sjb_bill_cus_title,sjb_bill_idtype,sjb_bill_id,sjb_bill_fax,sjb_bill_town,sjb_msn_no,sjb_isexternalitem,sjb_itemtype,sjb_bill_mobileno,sjb_agreementno,sjb_email,sjb_bill_email,sjb_inform_person,sjb_inform_contact,sjb_inform_location,sjb_isagreement,sjb_cusagreementno,sjb_quotation,sjb_substage) " +
             " values (:sjb_jobno, :sjb_date, :sjb_location, :sjb_company, " +
             " :sjb_channel, :sjb_jobcategory, :sjb_jobtype, :sjb_jobsubtype, " +
             " :sjb_manualref, :sjb_otherref, :sjb_reqno, :sjb_jobstage, " +
             " :sjb_remarks, :sjb_prority, :sjb_startdate, :sjb_cusexpectdate, " +
             " :sjb_noofprint, :sjb_lastprintby, :sjb_orderno, :sjb_mainreqno, " +
             " :sjb_mainjobno, :sjb_mainreqlocation, :sjb_custcode, " +
             " :sjb_custtitle, :sjb_custname, :sjb_idtype, :sjb_idno, " +
             " :sjb_address1, :sjb_address2, :sjb_town, :sjb_phoneno, " +
             " :sjb_faxno, :sjb_mobino, :sjb_cont_person, :sjb_cont_address1, " +
             " :sjb_cont_address2, :sjb_cont_phone_no, :sjb_job_remarks, " +
             " :sjb_tech_remarks, :sjb_bill_cus_code, :sjb_bill_cus_name, " +
             " :sjb_bill_address1, :sjb_bill_address2, :sjb_bill_address3, " +
             " :sjb_bill_phone_no, :sjb_aodissueloca, :sjb_aodissuedate, " +
             " :sjb_aodissueno, :sjb_aodrecno, :sjb_status, :sjb_createby, " +
             " :sjb_createwhen, :sjb_lastmodifyby, :sjb_lastmodifywhen ,:sjb_profitcenter," +
             " :sjb_insucompany,:sjb_bill_cus_title,:sjb_bill_idtype,:sjb_bill_id,:sjb_bill_fax,:sjb_bill_town,:sjb_msn_no,:sjb_isexternalitem,:sjb_itemtype,:sjb_bill_mobileno,:sjb_agreementno,:sjb_email,:sjb_bill_email,:sjb_inform_person,:sjb_inform_contact,:sjb_inform_location,:sjb_isagreement,:sjb_cusagreementno,:sjb_quotation,:sjb_substage)";

            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":sjb_jobno", OracleDbType.NVarchar2).Value = _header.Sjb_jobno;
            _oCom.Parameters.Add(":sjb_date", OracleDbType.Date).Value = _header.Sjb_date;
            _oCom.Parameters.Add(":sjb_location", OracleDbType.NVarchar2).Value = _header.Sjb_location;
            _oCom.Parameters.Add(":sjb_company", OracleDbType.NVarchar2).Value = _header.Sjb_company;
            _oCom.Parameters.Add(":sjb_channel", OracleDbType.NVarchar2).Value = _header.Sjb_channel;
            _oCom.Parameters.Add(":sjb_jobcategory", OracleDbType.NVarchar2).Value = _header.Sjb_jobcategory;
            _oCom.Parameters.Add(":sjb_jobtype", OracleDbType.NVarchar2).Value = _header.Sjb_jobtype;
            _oCom.Parameters.Add(":sjb_jobsubtype", OracleDbType.NVarchar2).Value = _header.Sjb_jobsubtype;
            _oCom.Parameters.Add(":sjb_manualref", OracleDbType.NVarchar2).Value = _header.Sjb_manualref;
            _oCom.Parameters.Add(":sjb_otherref", OracleDbType.NVarchar2).Value = _header.Sjb_otherref;
            _oCom.Parameters.Add(":sjb_reqno", OracleDbType.NVarchar2).Value = _header.Sjb_reqno;
            _oCom.Parameters.Add(":sjb_jobstage", OracleDbType.Decimal).Value = _header.Sjb_jobstage;
            _oCom.Parameters.Add(":sjb_remarks", OracleDbType.NVarchar2).Value = _header.Sjb_remarks;
            _oCom.Parameters.Add(":sjb_prority", OracleDbType.NVarchar2).Value = _header.Sjb_prority;
            _oCom.Parameters.Add(":sjb_startdate", OracleDbType.Date).Value = _header.Sjb_startdate;
            _oCom.Parameters.Add(":sjb_cusexpectdate", OracleDbType.Date).Value = _header.Sjb_cusexpectdate;
            _oCom.Parameters.Add(":sjb_noofprint", OracleDbType.Int32).Value = _header.Sjb_noofprint;
            _oCom.Parameters.Add(":sjb_lastprintby", OracleDbType.NVarchar2).Value = _header.Sjb_lastprintby;
            _oCom.Parameters.Add(":sjb_orderno", OracleDbType.NVarchar2).Value = _header.Sjb_orderno;
            _oCom.Parameters.Add(":sjb_mainreqno", OracleDbType.NVarchar2).Value = _header.Sjb_mainreqno;
            _oCom.Parameters.Add(":sjb_mainjobno", OracleDbType.NVarchar2).Value = _header.Sjb_mainjobno;
            _oCom.Parameters.Add(":sjb_mainreqlocation", OracleDbType.NVarchar2).Value = _header.Sjb_mainreqlocation;
            _oCom.Parameters.Add(":sjb_custcode", OracleDbType.NVarchar2).Value = _header.Sjb_custcode;
            _oCom.Parameters.Add(":sjb_custtitle", OracleDbType.NVarchar2).Value = _header.Sjb_custtitle;
            _oCom.Parameters.Add(":sjb_custname", OracleDbType.NVarchar2).Value = _header.Sjb_custname;
            _oCom.Parameters.Add(":sjb_idtype", OracleDbType.NVarchar2).Value = _header.Sjb_idtype;
            _oCom.Parameters.Add(":sjb_idno", OracleDbType.NVarchar2).Value = _header.Sjb_idno;
            _oCom.Parameters.Add(":sjb_address1", OracleDbType.NVarchar2).Value = _header.Sjb_address1;
            _oCom.Parameters.Add(":sjb_address2", OracleDbType.NVarchar2).Value = _header.Sjb_address2;
            _oCom.Parameters.Add(":sjb_town", OracleDbType.NVarchar2).Value = _header.Sjb_town;
            _oCom.Parameters.Add(":sjb_phoneno", OracleDbType.NVarchar2).Value = _header.Sjb_phoneno;
            _oCom.Parameters.Add(":sjb_faxno", OracleDbType.NVarchar2).Value = _header.Sjb_faxno;
            _oCom.Parameters.Add(":sjb_mobino", OracleDbType.NVarchar2).Value = _header.Sjb_mobino;
            _oCom.Parameters.Add(":sjb_cont_person", OracleDbType.NVarchar2).Value = _header.Sjb_cont_person;
            _oCom.Parameters.Add(":sjb_cont_address1", OracleDbType.NVarchar2).Value = _header.Sjb_cont_address1;
            _oCom.Parameters.Add(":sjb_cont_address2", OracleDbType.NVarchar2).Value = _header.Sjb_cont_address2;
            _oCom.Parameters.Add(":sjb_cont_phone_no", OracleDbType.NVarchar2).Value = _header.Sjb_cont_phone_no;
            _oCom.Parameters.Add(":sjb_job_remarks", OracleDbType.NVarchar2).Value = _header.Sjb_job_remarks;
            _oCom.Parameters.Add(":sjb_tech_remarks", OracleDbType.NVarchar2).Value = _header.Sjb_tech_remarks;
            _oCom.Parameters.Add(":sjb_bill_cus_code", OracleDbType.NVarchar2).Value = _header.Sjb_bill_cus_code;
            _oCom.Parameters.Add(":sjb_bill_cus_name", OracleDbType.NVarchar2).Value = _header.Sjb_bill_cus_name;
            _oCom.Parameters.Add(":sjb_bill_address1", OracleDbType.NVarchar2).Value = _header.Sjb_bill_address1;
            _oCom.Parameters.Add(":sjb_bill_address2", OracleDbType.NVarchar2).Value = _header.Sjb_bill_address2;
            _oCom.Parameters.Add(":sjb_bill_address3", OracleDbType.NVarchar2).Value = _header.Sjb_bill_address3;
            _oCom.Parameters.Add(":sjb_bill_phone_no", OracleDbType.NVarchar2).Value = _header.Sjb_bill_phone_no;
            _oCom.Parameters.Add(":sjb_aodissueloca", OracleDbType.NVarchar2).Value = _header.Sjb_aodissueloca;
            _oCom.Parameters.Add(":sjb_aodissuedate", OracleDbType.Date).Value = _header.Sjb_aodissuedate;
            _oCom.Parameters.Add(":sjb_aodissueno", OracleDbType.NVarchar2).Value = _header.Sjb_aodissueno;
            _oCom.Parameters.Add(":sjb_aodrecno", OracleDbType.NVarchar2).Value = _header.Sjb_aodrecno;
            _oCom.Parameters.Add(":sjb_status", OracleDbType.NVarchar2).Value = _header.Sjb_status;
            _oCom.Parameters.Add(":sjb_createby", OracleDbType.NVarchar2).Value = _header.Sjb_createby;
            _oCom.Parameters.Add(":sjb_createwhen", OracleDbType.Date).Value = _header.Sjb_createwhen;
            _oCom.Parameters.Add(":sjb_lastmodifyby", OracleDbType.NVarchar2).Value = _header.Sjb_lastmodifyby;
            _oCom.Parameters.Add(":sjb_lastmodifywhen", OracleDbType.Date).Value = _header.Sjb_lastmodifywhen;
            _oCom.Parameters.Add(":sjb_profitcenter", OracleDbType.NVarchar2).Value = _header.Sjb_profitcenter;
            _oCom.Parameters.Add(":sjb_insucompany", OracleDbType.NVarchar2).Value = _header.Sjb_insucompany;
            _oCom.Parameters.Add(":sjb_bill_cus_title", OracleDbType.NVarchar2).Value = _header.Sjb_bill_cus_title;
            _oCom.Parameters.Add(":sjb_bill_idtype", OracleDbType.NVarchar2).Value = _header.Sjb_bill_idtype;
            _oCom.Parameters.Add(":sjb_bill_id", OracleDbType.NVarchar2).Value = _header.Sjb_bill_id;
            _oCom.Parameters.Add(":sjb_bill_fax", OracleDbType.NVarchar2).Value = _header.Sjb_bill_fax;
            _oCom.Parameters.Add(":sjb_bill_town", OracleDbType.NVarchar2).Value = _header.Sjb_bill_town;
            _oCom.Parameters.Add(":sjb_msn_no", OracleDbType.NVarchar2).Value = _header.Sjb_msn_no;
            _oCom.Parameters.Add(":sjb_isexternalitem", OracleDbType.Int32).Value = _header.Sjb_isexternalitem;
            _oCom.Parameters.Add(":sjb_itemtype", OracleDbType.NVarchar2).Value = _header.Sjb_itemtype;
            _oCom.Parameters.Add(":sjb_bill_mobileno", OracleDbType.NVarchar2).Value = _header.Sjb_bill_mobileno;
            _oCom.Parameters.Add(":sjb_agreementno", OracleDbType.NVarchar2).Value = _header.Sjb_agreementno;
            _oCom.Parameters.Add(":sjb_email", OracleDbType.NVarchar2).Value = _header.Sjb_email;
            _oCom.Parameters.Add(":sjb_bill_email", OracleDbType.NVarchar2).Value = _header.Sjb_bill_email;
            _oCom.Parameters.Add(":sjb_inform_person", OracleDbType.NVarchar2).Value = _header.Sjb_inform_person;
            _oCom.Parameters.Add(":sjb_inform_contact", OracleDbType.NVarchar2).Value = _header.Sjb_inform_contact;
            _oCom.Parameters.Add(":sjb_inform_location", OracleDbType.NVarchar2).Value = _header.Sjb_inform_location;
            _oCom.Parameters.Add(":sjb_isagreement", OracleDbType.NVarchar2).Value = _header.Sjb_isagreement;
            _oCom.Parameters.Add(":sjb_cusagreementno", OracleDbType.NVarchar2).Value = _header.Sjb_cusagreementno;
            _oCom.Parameters.Add(":sjb_quotation", OracleDbType.NVarchar2).Value = _header.Sjb_quotation;
            _oCom.Parameters.Add(":sjb_substage", OracleDbType.NVarchar2).Value = _header.Sjb_substage;

            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveServiceJobDetails(SCMServiceJobDetails _details)
        {
            string sql = " insert into ser_job_detail " +
                " (jbd_jobno, jbd_jobline, jbd_itemcode, jbd_itemstatus," +
                " jbd_itemdesc, jbd_brand, jbd_model, jbd_itemcost," +
                " jbd_serial1, jbd_serial2, jbd_warranty, jbd_regno," +
                " jbd_milage, jbd_warranty_status, jbd_onloan," +
                " jbd_sentwcn," +
                " jbd_isinsurance, jbd_req_type, jbd_ser_term," +
                " jbd_lastwarrstdate, jbd_issued, jbd_mainitemcode," +
                " jbd_mainitemserial, jbd_mainitemwarranty, jbd_isstockupdate," +
                " jbd_itemmfc, jbd_mainitemmfc, jbd_availabilty, jbd_usejob," +
                " jbd_msnno, jbd_itemtype, jbd_serlocachr, jbd_customernotes," +
                " jbd_mainreqno, jbd_mainreqlocation, jbd_mainjobno," +
                " jbd_reqitemtype,jbd_reqno,jbd_reqline,jbd_needgatepass,jbd_warrperiod,jbd_warrstartdate ) " +
            " VALUES ('" + _details.Jbd_jobno + "', " + _details.Jbd_jobline + ", '" + _details.Jbd_itemcode + "', '" + _details.Jbd_itemstatus + "'," +
                " '" + _details.Jbd_itemdesc + "', '" + _details.Jbd_brand + "', '" + _details.Jbd_model + "', " + _details.Jbd_itemcost + "," +
                " '" + _details.Jbd_serial1 + "','" + _details.Jbd_serial2 + "', '" + _details.Jbd_warranty + "', '" + _details.Jbd_regno + "'," +
                " " + _details.Jbd_milage + ", " + Convert.ToInt32(_details.Jbd_warranty_status) + ", " + Convert.ToInt32(_details.Jbd_onloan) + "," +
                " " + Convert.ToInt32(_details.Jbd_sentwcn) + "," +
                " " + Convert.ToInt32(_details.Jbd_isinsurance) + ", '" + _details.Jbd_req_type + "', " + _details.Jbd_ser_term + "," +
                " TO_DATE('" + _details.Jbd_lastwarrstdate.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')," + Convert.ToInt32(_details.Jbd_issued) + ",'" + _details.Jbd_mainitemcode + "'," +
                " '" + _details.Jbd_mainitemserial + "','" + _details.Jbd_mainitemwarranty + "', " + Convert.ToInt32(_details.Jbd_isstockupdate) + "," +
                " '" + _details.Jbd_itemmfc + "', '" + _details.Jbd_mainitemmfc + "', " + Convert.ToInt32(_details.Jbd_availabilty) + ", '" + _details.Jbd_usejob + "'," +
                " '" + _details.Jbd_msnno + "', '" + _details.Jbd_itemtype + "', '" + _details.Jbd_serlocachr + "', '" + _details.Jbd_customernotes + "'," +
                " '" + _details.Jbd_mainreqno + "', '" + _details.Jbd_mainreqlocation + "', '" + _details.Jbd_mainjobno + "','" + _details.Jbd_reqitemtype + "','" + _details.Jbd_reqno + "'," + _details.Jbd_reqline + ",0," + _details.Jbd_warrperiod + " , TO_DATE('" + _details.Jbd_chg_warrstdate.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')) ";
            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveServiceJobItems(SCMServiceJobDefects _defect)
        {
            string sql = " insert into ser_job_def_items " +
            " (srd_job_no, srd_job_line, srd_def_line, srd_company, srd_job_def_type, srd_job_def_remarks) " +
            " values     ('" + _defect.Srd_job_no + "', " + _defect.Srd_job_line + ", " + _defect.Srd_def_line + ", '" + _defect.Srd_company + "','" + _defect.Srd_job_def_type + "', '" + _defect.Srd_job_def_remarks + "') ";

            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveServiceStageLog(SCMServiceStageLog _stage)
        {
            string sql = " INSERT INTO ser_job_stage_log " +
            " (sjl_seqno, sjl_reqno, sjl_jobno, sjl_location, " +
            " sjl_otherdocno, sjl_jobstage, sjl_createby, sjl_createwhen " +
            " ) " +
            " VALUES (ser_seqjobstagelog.nextval, :sjl_reqno, :sjl_jobno, :sjl_location, " +
            " :sjl_otherdocno, :sjl_jobstage, :sjl_createby, current_date " +
            " )";

            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":sjl_reqno", OracleDbType.NVarchar2).Value = _stage.Sjl_reqno;
            _oCom.Parameters.Add(":sjl_jobno", OracleDbType.NVarchar2).Value = _stage.Sjl_jobno;
            _oCom.Parameters.Add(":sjl_location", OracleDbType.NVarchar2).Value = _stage.Sjl_location;
            _oCom.Parameters.Add(":sjl_otherdocno", OracleDbType.NVarchar2).Value = _stage.Sjl_otherdocno;
            _oCom.Parameters.Add(":sjl_jobstage", OracleDbType.Decimal).Value = _stage.Sjl_jobstage;
            _oCom.Parameters.Add(":sjl_createby", OracleDbType.NVarchar2).Value = _stage.Sjl_createby;

            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int InsertUpdateServiceSchedule(SCMServiceSchedule _schedule, bool isUpdate)
        {
            string sql = "";
            if (!isUpdate)
            {
                sql = "insert into TEMP_WARA_UPLOAD_SERV(warr_no, serial_no, item_code, item_status, service_term," +
                             " wr_period_from, wr_period_to, wr_period_uom," +
                             "wr_period_alt_from, wr_period_alt_to, wr_period_alt_uom, is_free," +
                             "job_date, job_no, actual_reading) values" +
                             "('" + _schedule.Warr_no + "','" + _schedule.Serial_no + "','" + _schedule.Item_code + "','" + _schedule.Item_status + "'," +
                             "" + _schedule.Service_term + "," + _schedule.Wr_period_from + " ," + _schedule.Wr_period_to + "" +
                             " ,'" + _schedule.Wr_period_uom + "'," + _schedule.Wr_period_alt_from + " ," + _schedule.Wr_period_alt_to + "" +
                              ",'" + _schedule.Wr_period_alt_uom + "'," + Convert.ToInt32(_schedule.Is_free) + ",TO_DATE('" + _schedule.Job_date.ToString("dd/MM/yyyy") + "','DD/MM/YYYY'),'" + _schedule.Job_no + "'," + _schedule.Actual_reading + ")";
            }
            else
            {
                sql = "update  TEMP_WARA_UPLOAD_SERV set job_no='" + _schedule.Job_no + "', job_date=TO_DATE('" + _schedule.Job_date.ToString("dd/MM/yyyy") + "'),actual_reading=" + _schedule.Actual_reading + "" +
                    "where ITEM_CODE='" + _schedule.Item_code + "' and SERIAL_NO='" + _schedule.Serial_no + "'";
            }
            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveServiceRequest(SCMServiceRequestHeader _request)
        {
            string sql = "insert into ser_req_header(srh_req_no,srh_company,srh_location,srh_date,srh_channel,srh_manualref,srh_reqtype,srh_reqcategory,srh_priority,srh_otherref,srh_custcode,srh_custtitle,srh_custname,srh_idtype,srh_idno,srh_address1,srh_address2,srh_town,srh_phoneno,srh_cont_person,srh_cont_address,srh_cont_phone_no,srh_remarks,srh_status,srh_req_remarks,srh_req_sub_type,srh_reqtime,srh_tech_remarks,srh_createby,srh_createwhen,srh_lastmodifyby,srh_lastmodifywhen,srh_bill_cus_code,srh_bill_cus_name,srh_bill_address1,srh_bill_address2,srh_bill_address3,srh_bill_phone_no,srh_allowedit,srh_mainreqno,srh_mainreqlocation,srh_cusexpectdate,srh_profitcenter,srh_isexternalitem,srh_insucompany,srh_bill_cus_title,srh_bill_idtype,srh_bill_id,srh_bill_fax,srh_bill_town, srh_email,srh_bill_email,srh_agreementno,srh_inform_person,srh_inform_contact,srh_inform_location) values " +
        "('" + _request.Srh_req_no + "','" + _request.Srh_company + "','" + _request.Srh_location + "',TO_DATE('" + _request.Srh_date.ToString("dd/MM/yyyy") + "','dd/MM/yyyy'),'" + _request.Srh_channel + "','" + _request.Srh_manualref + "','" + _request.Srh_reqtype + "','" + _request.Srh_reqcategory + "','" + _request.Srh_priority + "','" + _request.Srh_otherref + "','" + _request.Srh_custcode + "','" + _request.Srh_custtitle + "','" + _request.Srh_custname + "','" + _request.Srh_idtype + "','" + _request.Srh_idno + "','" + _request.Srh_address1 + "','" + _request.Srh_address2 + "','" + _request.Srh_town + "','" + _request.Srh_phoneno + "','" + _request.Srh_cont_person + "','" + _request.Srh_cont_address + "','" + _request.Srh_cont_phone_no + "','" + _request.Srh_remarks + "','" + _request.Srh_status + "','" + _request.Srh_req_remarks + "','" + _request.Srh_req_sub_type + "',Current_Date,'" + _request.Srh_tech_remarks + "','" + _request.Srh_createby + "',current_date,'" + _request.Srh_lastmodifyby + "',current_date,'" + _request.Srh_bill_cus_code + "','" + _request.Srh_bill_cus_name + "','" + _request.Srh_bill_address1 + "','" + _request.Srh_bill_address2 + "','" + _request.Srh_bill_address3 + "','" + _request.Srh_bill_phone_no + "'," + Convert.ToInt32(_request.Srh_allowedit) + ",'" + _request.Srh_mainreqno + "','" + _request.Srh_mainreqlocation + "',TO_DATE('" + _request.Srh_cusexpectdate.ToString("dd/MM/yyyy") + "','dd/MM/yyyy'),'" + _request.Srh_profitcenter + "'," + Convert.ToInt32(_request.Srh_isexternalitem) + ",'" + _request.Srh_insucompany + "','" + _request.Srh_bill_cus_title + "','" + _request.Srh_bill_idtype + "','" + _request.Srh_bill_id + "','" + _request.Srh_bill_fax + "','" + _request.Srh_bill_town + "','" + _request.Srh_email + "','" + _request.Srh_bill_email + "','" + _request.Srh_agreementno + "','" + _request.Srh_inform_person + "','" + _request.Srh_inform_contact + "','" + _request.Srh_inform_location + "')";
            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveRequestDetail(SCMServiceRequestDetails _details)
        {
            string sql = "INSERT INTO SER_REQ_ITEMS(SRI_REQ_NO,SRI_REQ_LINE,SRI_ITEMCODE,SRI_ITEMSTATUS ,SRI_ITEMDESC,SRI_BRAND,SRI_MODEL,SRI_ITEMCOST,SRI_SERIAL1,SRI_SERIAL2,SRI_WARRANTY,SRI_REGNO,SRI_MILAGE,SRI_WARRANTY_STATUS,SRI_MAINITEMCODE,SRI_MAINITEMSERIAL,SRI_MAINITEMWARRANTY,SRI_ISSTOCKUPDATE,SRI_AVAILABILTY,SRI_ITEMTYPE,SRI_MAINREQNO,SRI_MAINREQLOCATION,sri_reqitemtype,sri_usejob,sri_warrstartdate,sri_serlocachr,sri_mainjobno) VALUES " +
                      "('" + _details.Sri_req_no + "'," + _details.Sri_req_line + ",'" + _details.Sri_itemcode + "','" + _details.Sri_itemstatus + "', '" + _details.Sri_itemdesc + "','" + _details.Sri_brand + "','" + _details.Sri_model + "'," + _details.Sri_itemcost + ",'" + _details.Sri_serial1 + "','" + _details.Sri_serial2 + "','" + _details.Sri_warranty + "','" + _details.Sri_regno + "'," + _details.Sri_milage + ",'" + Convert.ToInt32(_details.Sri_warranty_status) + "','" + _details.Sri_mainitemcode + "','" + _details.Sri_mainitemserial + "','" + _details.Sri_mainitemwarranty + "'," + Convert.ToInt32(_details.Sri_isstockupdate) + "," + _details.Sri_availabilty + ",'" + _details.Sri_itemtype + "','" + _details.Sri_mainreqno + "','" + _details.Sri_mainreqlocation + "','JOB',1,to_date('" + _details.Sri_warrstartdate.ToString("dd/MM/yyyy") + "','DD/MM/YYYY'),'" + _details.Sri_serlocachr + "','" + _details.Sri_mainjobno + "')";
            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public int SaveRequestDefects(SCMServiceRequestDefect _defect)
        {
            string sql = "insert into ser_req_def_items " +
                       "( srd_req_no,srd_req_line ,  srd_def_line, srd_company, srd_req_def_type, srd_req_def_remarks, srd_job_def_type, srd_job_def_remarks,srd_act_def_type,srd_act_def_remarks ) values" +
                       "('" + _defect.Srd_req_no + "'," + _defect.Srd_def_line + ",'" + _defect.Srd_company + "','" + _defect.Srd_req_def_type + "','" + _defect.Srd_req_def_remarks + "','" + _defect.Srd_act_def_type + "','" + _defect.Srd_act_def_remarks + "')";

            OracleCommand _oCom = new OracleCommand(sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            int result = _oCom.ExecuteNonQuery();
            return result;
        }

        public DataTable GetMRN_Request(string _mrn)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mrn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mrn;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_mrn_dispatch", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable GetWarehouseRequest(string _dipatchReq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_request", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dipatchReq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_disp_ware_hdr", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable GetMovementHeader(string _warehouseReq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_war_request", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warehouseReq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_mov_by_req", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable GetDispatchDetails(string _req)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_disp_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_disp_det", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable GetWarehouseDetails(string _req)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_ware", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_disp_war_det", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable GetMovmentDetails(string _req)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "sp_get_mov_det", CommandType.StoredProcedure, false, param);

            return dt;
        }

        public DataTable CheckBondNo(string _company, string _bondNo)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("P_BONDNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bondNo;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl", "SP_CHECKBONDNO", CommandType.StoredProcedure, false, param);

            return dt;

            //CREATE OR REPLACE PROCEDURE SP_CHECKBONDNO(P_COM IN NVARCHAR2,  P_BONDNO IN NVARCHAR2,C_DATA OUT SYS_REFCURSOR)
            //IS
            //BEGIN
            //--Code by Chamal 21/06/2013
            //OPEN C_DATA FOR
            //SELECT WHF_COST_SHEET_HEADER.COST_SHEET_REF, WHF_BOND_HEADER.DOCUMENT_NO AS DOC_NO,
            //WHF_BOND_HEADER.DOCUMENT_DATE, WHF_BOND_HEADER.SUN_BOND_NO, WHF_BOND_HEADER.RELATED_DOCUMENT
            //FROM WHF_COST_SHEET_HEADER INNER JOIN WHF_BOND_HEADER ON WHF_COST_SHEET_HEADER.TO_BOND_NO = WHF_BOND_HEADER.DOCUMENT_NO
            //WHERE (WHF_COST_SHEET_HEADER.GRN_STATUS <> 'COMPLETE') AND (WHF_COST_SHEET_HEADER.STATUES = 'Y') AND
            //(WHF_BOND_HEADER.COMPANY_CODE =P_COM) AND (WHF_BOND_HEADER.DOCUMENT_NO =P_BONDNO);
            //END;
        }

        public DataTable GetBondItems(string _bondNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bondno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bondNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetBondItems", CommandType.StoredProcedure, false, param);
            return dt;
        }

        //Chamal 12/01/2014
        public DataTable GetLOPOItems(string _bondNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bondno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bondNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetSCMPOItems", CommandType.StoredProcedure, false, param);
            return dt;
        }

        //Chamal 12/01/2014
        public DataTable GetCONSQUOItems(string _bondNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bondno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bondNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetSCMCONSQUOItems", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public int Update_Bond_Status(string _bondNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bond_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bondNo;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("SP_UPDATEBONDSTATUS", CommandType.StoredProcedure, param);
        }

        //Chamal 26/07/2013
        public DataTable GetInvoiceHeader(string _invcNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invcno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invcNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetInvoiceHeader", CommandType.StoredProcedure, false, param);
            return dt;
        }

        //Chamal 26/07/2013
        public DataTable GetInvoiceItems(string _invcNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invcno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invcNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetInvoiceItems", CommandType.StoredProcedure, false, param);
            return dt;
        }

        //Chamal 26/07/2013
        public DataTable GetCustomerDetails(string _com, string _custCode)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "GetCustomerDetails", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetInventoryTrackerSearchData_NEW2(string _initialSearchParams)
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

            //Set relavant parameters according to the,search catergory.

            //Modify parameter values for LIKE search.
            //   searchParams[0] = (searchParams[0] != "" || searchParams[0] != null) ? (searchParams[0].ToUpper() + "%") : "%";
            //   searchParams[1] = (searchParams[1] != "" || searchParams[1] != null) ? (searchParams[1].ToUpper() + "%") : "%";
            //   searchParams[2] = (searchParams[2] != "" || searchParams[2] != null) ? (searchParams[2].ToUpper() + "%") : "%";
            ////   searchParams[3] = (searchParams[3] != "" || searchParams[3] != null) ? (searchParams[3].ToUpper() + "%") : "%";
            //   searchParams[4] = (searchParams[4] != "" || searchParams[4] != null) ? (searchParams[4].ToUpper() + "%") : "%";
            //   searchParams[5] = (searchParams[5] != "" || searchParams[5] != null) ? (searchParams[5].ToUpper() + "%") : "%";
            //   searchParams[6] = (searchParams[6] != "" || searchParams[6] != null) ? (searchParams[6].ToUpper() + "%") : "%";
            //   searchParams[7] = (searchParams[7] != "" || searchParams[7] != null) ? (searchParams[7].ToUpper() + "%") : "%";
            //   searchParams[8] = (searchParams[8] != "" || searchParams[8] != null) ? (searchParams[8].ToUpper() + "%") : "%";
            //   searchParams[9] = (searchParams[9] != "" || searchParams[9] != null) ? (searchParams[9].ToUpper() + "%") : "%";
            //   searchParams[10] = (searchParams[10] != "" || searchParams[10] != null) ? (searchParams[10].ToUpper() + "%") : "%";
            //   searchParams[11] = (searchParams[11] != "" || searchParams[11] != null) ? (searchParams[11]) : "%";
            //   searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : "%";
            //   searchParams[13] = (searchParams[13] != "" || searchParams[13] != null) ? (searchParams[13].ToUpper() + "%") : "%";
            searchParams[0] = (searchParams[0] != "" || searchParams[0] != null) ? (searchParams[0].ToUpper() + "%") : "%";
            searchParams[1] = (searchParams[1] != "" || searchParams[1] != null) ? (searchParams[1].ToUpper() + "%") : "%";
            searchParams[2] = (searchParams[2] != "" || searchParams[2] != null) ? (searchParams[2].ToUpper() + "%") : "%";
            //  searchParams[3] = (searchParams[3] != "" || searchParams[3] != null) ? (searchParams[3].ToUpper() + "%") : "%";
            searchParams[4] = (searchParams[4] != "" || searchParams[4] != null) ? (searchParams[4].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[5] = (searchParams[5] != "" || searchParams[5] != null) ? (searchParams[5].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[6] = (searchParams[6] != "" || searchParams[6] != null) ? (searchParams[6].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[7] = (searchParams[7] != "" || searchParams[7] != null) ? (searchParams[7].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[8] = (searchParams[8] != "" || searchParams[8] != null) ? (searchParams[8].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[9] = (searchParams[9] != "" || searchParams[9] != null) ? (searchParams[9].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[10] = (searchParams[10] != "" || searchParams[10] != null) ? (searchParams[10].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[11] = (searchParams[11] != "" || searchParams[11] != null) ? (searchParams[11]) : "%";
            //  searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : searchParams[4].ToUpper();
            searchParams[12] = (searchParams[12] != "" || searchParams[12] != null) ? (searchParams[12].ToUpper() + "%") : "%";
            searchParams[13] = (searchParams[13] != "" || searchParams[13] != null) ? (searchParams[13].ToUpper() + "%") : "%";
            searchParams[14] = (searchParams[14] != "" || searchParams[14] != null) ? (searchParams[14].ToUpper() + "%") : "%";

            //Updated by akila 2017/12/05
            string _colorCode = "%";
            if (searchParams != null && searchParams.Length > 17)
            {
                _colorCode = (searchParams[18] != "" || searchParams[18] != null) ? (searchParams[18].ToUpper() + "%") : "%";
            }

            OracleParameter[] param = new OracleParameter[18];
            (param[0] = new OracleParameter("P_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0];
            (param[1] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1];
            (param[2] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[14];
            (param[3] = new OracleParameter("p_ststus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2];
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[3];
            (param[5] = new OracleParameter("p_main", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[4];
            (param[6] = new OracleParameter("p_sub", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[5];
            (param[7] = new OracleParameter("p_cha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[6];
            (param[8] = new OracleParameter("p_subcha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[7];
            (param[9] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[8];
            (param[10] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[9];
            (param[11] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[10];
            (param[12] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[11];
            (param[13] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[12];
            (param[14] = new OracleParameter("p_sub1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[13];

            if (Convert.ToBoolean(searchParams[15]) == true)
            {
                (param[15] = new OracleParameter("p_isCost", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            }
            else 
            { 
                (param[15] = new OracleParameter("p_isCost", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0; 
            }

            (param[16] = new OracleParameter("p_color", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _colorCode;//Updated by akila 2017/12/05
            param[17] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblscm", "sp_searchinventoryitem_NEW", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public Int32 SaveCustomer(MasterBusinessEntity _customer)
        {
            //sp_savescm2customer(

            //p_company_code          in cusdec_company_profile.company_code%type,
            //p_company_name          in cusdec_company_profile.company_name%type,
            //p_tin_no                in cusdec_company_profile.tin_no%type,
            //p_address_line_1        in cusdec_company_profile.address_line_1%type,
            //p_address_line_2        in cusdec_company_profile.address_line_2%type,
            //p_tel                   in cusdec_company_profile.tel%type,
            //p_fax                   in cusdec_company_profile.fax%type,
            //p_e_mail                in cusdec_company_profile.e_mail%type,
            //p_statues               in cusdec_company_profile.statues%type,
            //p_create_by             in cusdec_company_profile.create_by%type,

            //p_create_when           in cusdec_company_profile.create_when%type,
            //p_last_modify_by        in cusdec_company_profile.last_modify_by%type,
            //p_last_modify_when      in cusdec_company_profile.last_modify_when%type,
            //p_web_add               in cusdec_company_profile.web_add%type,
            //p_tax_reg               in cusdec_company_profile.tax_reg%type,
            //p_currency_code         in cusdec_company_profile.currency_code%type,
            //p_ref_no                in cusdec_company_profile.ref_no%type,
            //p_boi_reg_no            in cusdec_company_profile.boi_reg_no%type,
            //p_ppc_no                in cusdec_company_profile.ppc_no%type,
            //p_invoice_at_cost       in cusdec_company_profile.invoice_at_cost%type,

            //p_address_line_3        in cusdec_company_profile.address_line_3%type,
            //p_contacts              in cusdec_company_profile.contacts%type,
            //p_is_vat                in cusdec_company_profile.is_vat%type,
            //p_dis_rate              in cusdec_company_profile.dis_rate%type,
            //p_is_discount           in cusdec_company_profile.is_discount%type,
            //p_is_suspend            in cusdec_company_profile.is_suspend%type,
            //p_is_used               in cusdec_company_profile.is_used%type,
            //p_service_upload        in cusdec_company_profile.service_upload%type,
            //p_upload_date           in cusdec_company_profile.upload_date%type,
            //p_is_special_tax        in cusdec_company_profile.is_special_tax%type,

            //p_special_tax_rate      in cusdec_company_profile.special_tax_rate%type,
            //p_special_tax_rate_code in cusdec_company_profile.special_tax_rate_code%type,
            //p_tax_eccempted         in cusdec_company_profile.tax_eccempted%type,
            //p_supplier_com_code     in cusdec_company_profile.supplier_com_code%type,
            //p_is_cus_internal       in cusdec_company_profile.is_cus_internal%type,
            //p_foc_allowed           in cusdec_company_profile.foc_allowed%type,
            //p_customer_company      in cusdec_company_profile.customer_company%type,
            //p_town_code             in cusdec_company_profile.town_code%type,
            //p_pos_upload            in cusdec_company_profile.pos_upload%type,
            //p_pos_upload_when       in cusdec_company_profile.pos_upload_when%type,

            //p_credit_period         in cusdec_company_profile.credit_period%type,
            //p_is_customer           in cusdec_company_profile.is_customer%type,
            //p_inter_company_code    in cusdec_company_profile.inter_company_code%type,
            //p_cus_type              in cusdec_company_profile.cus_type%type,
            //p_cus_region            in cusdec_company_profile.cus_region%type,
            //p_mob_no                in cusdec_company_profile.mob_no%type,
            //p_is_svat               in cusdec_company_profile.is_svat%type,
            //p_stax_reg              in cusdec_company_profile.stax_reg%type,
            //p_cus_category          in cusdec_company_profile.cus_category%type,
            //o_effect                out NUMBER) is

            OracleParameter[] param = new OracleParameter[50];
            //company_code                   NVARCHAR2(20),
            (param[0] = new OracleParameter("p_company_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cd;
            //company_name                   NVARCHAR2(200) DEFAULT 'N/A',
            (param[1] = new OracleParameter("p_company_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_name) ? "." : _customer.Mbe_name;
            //tin_no                         NVARCHAR2(30) DEFAULT 'N/A',
            (param[2] = new OracleParameter("p_tin_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_tax_no) ? "." : _customer.Mbe_tax_no;
            //address_line_1                 NVARCHAR2(200) DEFAULT 'N/A',
            (param[3] = new OracleParameter("p_address_line_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_add1) ? "." : _customer.Mbe_add1;
            //address_line_2                 NVARCHAR2(200) DEFAULT 'N/A',
            (param[4] = new OracleParameter("p_address_line_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_add2) ? "." : _customer.Mbe_add2;
            //tel                            NVARCHAR2(50) DEFAULT 'N/A',
            (param[5] = new OracleParameter("p_tel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_tel) ? "." : _customer.Mbe_tel;
            //fax                            NVARCHAR2(50) DEFAULT 'N/A',
            (param[6] = new OracleParameter("p_fax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_fax) ? "." : _customer.Mbe_fax;
            //e_mail                         NVARCHAR2(50) DEFAULT 'N/A',
            (param[7] = new OracleParameter("p_e_mail", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_email) ? "." : _customer.Mbe_email;
            //statues                        CHAR(1 BYTE) DEFAULT 'E',
            (param[8] = new OracleParameter("p_statues", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = 1;
            //create_by                      NVARCHAR2(20) DEFAULT 'SCM',
            (param[9] = new OracleParameter("p_create_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_by;
            //create_when                    DATE DEFAULT CURRENT_DATE,
            (param[10] = new OracleParameter("p_create_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_dt;
            //last_modify_by                 NVARCHAR2(20) DEFAULT 'SCM',
            (param[11] = new OracleParameter("p_last_modify_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_by;
            //last_modify_when               DATE DEFAULT CURRENT_DATE,
            (param[12] = new OracleParameter("p_last_modify_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_dt;
            //web_add                        NVARCHAR2(50) DEFAULT 'SCM',
            (param[13] = new OracleParameter("p_web_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_wr_email;
            //tax_reg                        NVARCHAR2(50) DEFAULT 'N/A',
            (param[14] = new OracleParameter("p_tax_reg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_tax_no) ? "." : _customer.Mbe_tax_no;
            //currency_code                  NVARCHAR2(20) DEFAULT 'SCM',
            (param[15] = new OracleParameter("p_currency_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_com;
            //ref_no                         NVARCHAR2(10) DEFAULT 'N/A',
            (param[16] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_acc_cd;
            //boi_reg_no                     NVARCHAR2(30) DEFAULT 'N/A',
            (param[17] = new OracleParameter("p_boi_reg_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_tax_no) ? "." : _customer.Mbe_tax_no;
            //ppc_no                         NVARCHAR2(30) DEFAULT 'N/A',
            (param[18] = new OracleParameter("p_ppc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.IsNullOrEmpty(_customer.Mbe_pp_no) ? "." : _customer.Mbe_pp_no;
            //invoice_at_cost                NVARCHAR2(3) DEFAULT 'NO',
            (param[19] = new OracleParameter("p_invoice_at_cost", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "NO";
            //address_line_3                 NVARCHAR2(200) DEFAULT 'N/A',
            (param[20] = new OracleParameter("p_address_line_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cr_add1;
            //contacts                       NVARCHAR2(200) DEFAULT 'N/A',
            (param[21] = new OracleParameter("p_contacts", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_mob;
            //is_vat                         NUMBER(1,0) DEFAULT 0,
            (param[22] = new OracleParameter("p_is_vat", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _customer.Mbe_is_tax ? 1 : 0;
            //dis_rate                       NUMBER(30,10) DEFAULT 0,
            (param[23] = new OracleParameter("p_dis_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = 0;
            //is_discount                    NUMBER(1,0) DEFAULT 0,
            (param[24] = new OracleParameter("p_is_discount", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //is_suspend                     NUMBER(1,0) DEFAULT 0,
            (param[25] = new OracleParameter("p_is_suspend", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _customer.Mbe_is_suspend ? 1 : 0;
            //is_used                        NUMBER(1,0) DEFAULT 1,
            (param[26] = new OracleParameter("p_is_used", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //service_upload                 NUMBER(1,0) DEFAULT 0,
            (param[27] = new OracleParameter("p_service_upload", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //upload_date                    DATE,
            (param[28] = new OracleParameter("p_upload_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_dt;
            //is_special_tax                 NUMBER(1,0) DEFAULT 0,
            (param[29] = new OracleParameter("p_is_special_tax", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _customer.Mbe_is_svat ? 1 : 0;
            //special_tax_rate               NUMBER(30,10) DEFAULT 0,
            (param[30] = new OracleParameter("p_special_tax_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = 0;
            //special_tax_rate_code          NVARCHAR2(5) DEFAULT 'N/A',
            (param[31] = new OracleParameter("p_special_tax_rate_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.Empty;
            //tax_eccempted                  NUMBER(1,0) DEFAULT 0,
            (param[32] = new OracleParameter("p_tax_eccempted", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _customer.Mbe_tax_ex ? 1 : 0;
            //supplier_com_code              NVARCHAR2(10) DEFAULT 'ABL',
            (param[33] = new OracleParameter("p_supplier_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_com;
            //is_cus_internal                NUMBER(1,0) DEFAULT 0,
            (param[34] = new OracleParameter("p_is_cus_internal", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //foc_allowed                    NUMBER(1,0) DEFAULT 0,
            (param[35] = new OracleParameter("p_foc_allowed", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //customer_company               NVARCHAR2(20) DEFAULT 'N/A',
            (param[36] = new OracleParameter("p_customer_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_com;
            //town_code                      NVARCHAR2(50) DEFAULT 'N/A',
            (param[37] = new OracleParameter("p_town_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_town_cd;
            //pos_upload                     NUMBER(1,0) DEFAULT 0,
            (param[38] = new OracleParameter("p_pos_upload", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //pos_upload_when                DATE,
            (param[39] = new OracleParameter("p_pos_upload_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _customer.Mbe_cre_dt;
            //credit_period                  NUMBER(10,0) DEFAULT 0,
            (param[40] = new OracleParameter("p_credit_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
            //is_customer                    NUMBER(1,0) DEFAULT 1,
            (param[41] = new OracleParameter("p_is_customer", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            //inter_company_code             NVARCHAR2(20),
            (param[42] = new OracleParameter("p_inter_company_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = string.Empty;
            //cus_type                       NVARCHAR2(20),
            (param[43] = new OracleParameter("p_cus_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_tp;
            //cus_region                     NVARCHAR2(20),
            (param[44] = new OracleParameter("p_cus_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cr_town_cd;
            //mob_no                         NVARCHAR2(15) DEFAULT 'N/A',
            (param[45] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_mob;
            //is_svat                        NUMBER(1,0) DEFAULT 0,
            (param[46] = new OracleParameter("p_is_svat", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _customer.Mbe_is_svat ? 1 : 0;
            //stax_reg                       NVARCHAR2(50) DEFAULT 'N/A',
            (param[47] = new OracleParameter("p_stax_reg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_svat_no;
            //cus_category                   NVARCHAR2(100))
            (param[48] = new OracleParameter("p_cus_category", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer.Mbe_cate;

            param[49] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_savescm2customer", CommandType.StoredProcedure, param);

        }

        public DataTable GetWarrantySearch(string _company, string _serial1, string _serial2, string _warranty, string _invoice)
        {
            //sp_getwarrantysearch(p_com in NVARCHAR2,p_serial1 in NVARCHAR2,p_serial2 in NVARCHAR2,p_warranty in NVARCHAR2,p_invoice in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_serial1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial1;
            (param[2] = new OracleParameter("p_serial2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial2;
            (param[3] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            (param[4] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getwarrantysearch", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetWarrantySearchAll(string _company, string _serial1, string _serial2, string _warranty, string _invoice)
        {
            //sp_getwarrantysearch(p_com in NVARCHAR2,p_serial1 in NVARCHAR2,p_serial2 in NVARCHAR2,p_warranty in NVARCHAR2,p_invoice in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_serial1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial1;
            (param[2] = new OracleParameter("p_serial2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial2;
            (param[3] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            (param[4] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getwarrantysearchall", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMCustomer(string _customer)
        {
            //sp_getcustomerdet(p_customer in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getcustomerdet", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMInvoiceDetail(string _invoice)
        {
            //sp_getscmInvoicedetail(p_invoice in NVARCHAR2,c_data out sys_refcursor) is

            if (_invoice.ToUpper() == "N/A") return null;
            if (_invoice == ".") return null;
            if (_invoice.Length < 5) return null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getscmInvoicedetail", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable GetSCMDeliveryDetail(string _invoice)
        {
            //sp_getdeliverydet(p_invoice in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getdeliverydet", CommandType.StoredProcedure, false, param);
            return dt;
        }
        public DataTable GetSCMDeliveryDetailItem(string _invoice,string _item)
        { 
            //Nadeeka 08-05-2015
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getdeliverydetItem", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public decimal GetSCMDeliveryItemCost(string _item, string _doc, string _status)
        {
            //getserialcost(p_item in NVARCHAR2,p_doc in NVARCHAR2,p_status in NVARCHAR2,c_data out sys_refcursor )
            OracleParameter[] param = new OracleParameter[4];
            decimal _cost = 0;

            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "getserialcost", CommandType.StoredProcedure, false, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                _cost = dt.Rows[0].Field<decimal>("unit_cost");
            }
            return _cost;
        }

        public Int32 UpdateExchangeWarranty(string _oldwarranty, string _newwarranty, string _document, DateTime _start, Int32 _period, string _customer, string _name, string _address, string _tel, string _invoice, string _shop, string _shopname, decimal _unitprice, string _status)
        {
            //sp_exchangewarranty(p_oldwarranty in NVARCHAR2,p_newwarrantyno in NVARCHAR2,p_docno in NVARCHAR2,
            //p_startdate in date,p_period in NUMBER, p_cuscode in NVARCHAR2,p_name in NVARCHAR2,p_address in NVARCHAR2,p_tel in NVARCHAR2,p_invoice in NVARCHAR2,
            //p_shop in NVARCHAR2,p_shopname in NVARCHAR2,p_unitprice in NUMBER,p_status in NVARCHAR2,
            //o_effect out NUMBER)

            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("p_oldwarranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _oldwarranty;
            (param[1] = new OracleParameter("p_newwarrantyno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _newwarranty;
            (param[2] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[3] = new OracleParameter("p_startdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _start.Date;
            (param[4] = new OracleParameter("p_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _period;
            (param[5] = new OracleParameter("p_cuscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[6] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _name;
            (param[7] = new OracleParameter("p_address", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _address;
            (param[8] = new OracleParameter("p_tel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tel;
            (param[9] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[10] = new OracleParameter("p_shop", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _shop;
            (param[11] = new OracleParameter("p_shopname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _shopname;
            (param[12] = new OracleParameter("p_unitprice", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _unitprice;
            (param[13] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[14] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int16)UpdateRecords("sp_exchangewarranty", CommandType.StoredProcedure, param);

        }

        public DataTable GetSCMCreditNote(string _invoice, string _customer)
        {
            //sp_getscmInvoicedetail(p_invoice in NVARCHAR2,c_data out sys_refcursor) is
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoice;
            (param[1] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getcreditnote", CommandType.StoredProcedure, false, param);
            return dt;
        }

        public DataTable IsUserEntryExist(string _company, string _item, string _type, string _value)
        {
            //sp_checkuserentry(p_com in NVARCHAR2,p_item in NVARCHAR2, p_type in NVARCHAR2,p_value in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[3] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _value;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("CHKUSERENTRY", "sp_checkuserentry", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable GetSCMDeliveryOrder(string _document, string _partycode)
        {
            //sp_getdeliveryorder(p_doc in NVARCHAR2,p_partycd in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[1] = new OracleParameter("p_partycd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _partycode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "sp_getdeliveryorder", CommandType.StoredProcedure, false, param);
            return dt;
        }
        //Nadeeka 27-02-2015
        public int checkIsConsign(string _document, string _partycode)
        {
            int rows_affected = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[1] = new OracleParameter("p_partycd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _partycode;
             param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            rows_affected = UpdateRecords("sp_check_isConsign", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public bool CheckIsSCM2AOD(string _document, out string _invoiceNo)
        {// Nadeeka 27-02-2015
            bool _isTrue = false;
            _invoiceNo = string.Empty;
            //sp_getdeliveryorder(p_doc in NVARCHAR2,p_partycd in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_do", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "check_intr_scm2_aod", CommandType.StoredProcedure, false, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _isTrue = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _invoiceNo = Convert.ToString(dt.Rows[i]["ITH_OTH_DOCNO"]);
                    }
                }
            }
            return _isTrue;
            //CREATE OR REPLACE PROCEDURE Check_Intr_SCM2_DO(p_do in NVARCHAR2,c_data out sys_refcursor) is
            //--Written by Chamal on 01/10/2014
            //begin
            //open c_data for
            //SELECT ITH_DOC_NO, ITH_OTH_DOCNO FROM INT_HDR@SCM2LINK
            //WHERE ITH_DIRECT=0 AND ITH_DOC_TP='DO' AND ITH_DOC_NO =p_do AND ITH_STUS IN
            //(SELECT RMS_CD FROM REF_MOV_STUS@SCM2LINK WHERE RMS_ISAPP=1);
            //END;
        }
        public bool CheckIsSCM2DO(string _document, out string _invoiceNo)
        {
            bool _isTrue = false;
            _invoiceNo = string.Empty;
            //sp_getdeliveryorder(p_doc in NVARCHAR2,p_partycd in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_do", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "Check_Intr_SCM2_DO", CommandType.StoredProcedure, false, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _isTrue = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _invoiceNo = Convert.ToString(dt.Rows[i]["ITH_OTH_DOCNO"]);
                    }
                }
            }
            return _isTrue;
            //CREATE OR REPLACE PROCEDURE Check_Intr_SCM2_DO(p_do in NVARCHAR2,c_data out sys_refcursor) is
            //--Written by Chamal on 01/10/2014
            //begin
            //open c_data for
            //SELECT ITH_DOC_NO, ITH_OTH_DOCNO FROM INT_HDR@SCM2LINK
            //WHERE ITH_DIRECT=0 AND ITH_DOC_TP='DO' AND ITH_DOC_NO =p_do AND ITH_STUS IN
            //(SELECT RMS_CD FROM REF_MOV_STUS@SCM2LINK WHERE RMS_ISAPP=1);
            //END;
        }

        public DataTable GetSCM2InvoiceDT(string _document)
        {
            //sp_getdeliveryorder(p_doc in NVARCHAR2,p_partycd in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_invc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = QueryDataTable("tbl", "SP_GETSCM2INVCDET", CommandType.StoredProcedure, false, param);
            return dt;
            //CREATE OR REPLACE PROCEDURE SP_GETSCM2INVCDET(p_invc in NVARCHAR2,c_data out sys_refcursor) is
            //--Written by Chamal on 01/10/2014
            //begin
            //    open c_data for
            //       SELECT SAT_ITM.SAD_ITM_LINE AS LINE_NO,SAT_ITM.SAD_ITM_CD AS ITEM_CODE,MST_ITM_STUS.MIS_LP_CD AS ITEM_STATUS,SAT_ITM.SAD_UNIT_RT AS UNIT_RATE,SAT_ITM.SAD_QTY AS QTY
            //       FROM SAT_ITM@SCM2LINK, MST_ITM_STUS@SCM2LINK
            //       WHERE SAT_ITM.SAD_ITM_STUS = MST_ITM_STUS.MIS_CD AND SAT_ITM.SAD_INV_NO =p_invc
            //       ORDER BY SAT_ITM.SAD_ITM_CD,MST_ITM_STUS.MIS_LP_CD,SAT_ITM.SAD_UNIT_RT,SAT_ITM.SAD_QTY;
            //END;
        }

        public int UpdateSGLDeliveryOrder(string _document, string _item, string _serial)
        {
            //sp_updatepickserial(p_doc in NVARCHAR2,p_serial in NVARCHAR2,p_item in NVARCHAR2,o_effect out NUMBER)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[1] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updatepickserial", CommandType.StoredProcedure, param);
        }


        public Int32 UpdateTempWaraUpload_new(Service_Req_Det _ReqDet, string p_transactionno, DateTime p_podate, string p_customerCD, string p_customerADD, string p_customerphoneno)
        {
            OracleParameter[] param = new OracleParameter[23];
            Int32 effects = 0;





            (param[0] = new OracleParameter("p_transactionno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_transactionno;
            (param[1] = new OracleParameter("p_warrantystartdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_lastwarr_stdt;
            (param[2] = new OracleParameter("p_warrantyperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_warrperiod;
            (param[3] = new OracleParameter("p_salesorderno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";

            (param[4] = new OracleParameter("p_customerphoneno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerphoneno;
            (param[5] = new OracleParameter("p_podate", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[6] = new OracleParameter("p_invoiceno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_invc_no;
            (param[7] = new OracleParameter("p_transaction_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_loc;
            //(param[11] = new OracleParameter("p_warrem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Str_warranty_remarks;
            (param[8] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_itm_cd;
            (param[9] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_ser1;
            //  (param[14] = new OracleParameter("p_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Str_Reg_No;
            (param[10] = new OracleParameter("p_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_itm_stus;
            (param[11] = new OracleParameter("p_unitprice", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_itm_cost;
            (param[12] = new OracleParameter("p_warrantyno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_warr;
            (param[13] = new OracleParameter("p_newwarno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_waraamd_dt;
            (param[14] = new OracleParameter("p_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[15] = new OracleParameter("p_grndate", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[16] = new OracleParameter("p_sup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ReqDet.Jrd_supp_cd;
            (param[17] = new OracleParameter("p_chasis", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[18] = new OracleParameter("p_accno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[19] = new OracleParameter("p_customercode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerCD;
            (param[20] = new OracleParameter("p_customeraddressinvoce", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerADD;
            (param[21] = new OracleParameter("p_customeraddressinvoce", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerphoneno;

            param[22] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_upd_temp_wara_upload", CommandType.StoredProcedure, param);
            return effects;
        }

    }

    //process for SCM1 inventory
    //Written by Prabhath
    public class Inventory : SCMBaseDAL
    {
        public SalesDAL _emsSalesDAL;

        #region Variables Scope

        private string _sql;
        private Int32 _ref = 0;

        // Item Master Variables
        private string itm_Desc = "";

        private string itm_Model = "";
        private string itm_Brand = "";
        private string itm_Cate_1 = "";
        private string itm_Cate_2 = "";
        private string itm_Cate_3 = "";
        private string itm_Uom = "";
        private string itm_Serialize = "";
        private string itm_Warranty = "";

        // Location Master Variables
        private string loc_Ope = "";

        private string loc_Chnl = "";

        //Movement Variable
        private Int16 Move_I_Line_No = 0;

        private Int16 Move_C_Line_No = 0;
        private Int16 Move_Fifo_Line_No = 0;
        private Int16 Move_S_Line_No = 0;

        //Login user
        private string p_LoginUser = "EMS";

        private string p_ComCostMethod = "FIFO";

        //Oracle Varialbe
        private OracleDataAdapter _oDa;

        private OracleDataReader _oRd;

        #endregion Variables Scope

        #region ** Upload Sales and Reversal **

        #region upload process

        public void upload_Sales(string _invoiceno, decimal TotalInvAmt)
        {
            int _ref = 0;
            int I, J = 0;

            double _InvSeqNo = 2000000000;

            string _uploadType = "";
            string _comCode = "";
            string _locCode = "";
            string _direct = "";
            string _RepCode = "";
            string _ems_InvNo = String.Empty;
            string _ems_CusCode = String.Empty;
            string _ems_CusName = String.Empty;
            string _ems_CusAddress = String.Empty;
            string _ems_PC = String.Empty;
            string para_sale_Status = "N";
            Int16 para_TaxInv = 0;

            _oDa = new OracleDataAdapter();
            DataSet _emsData = new DataSet();

            // _emsDAL.ConnectionOpen();

            _sql = "SELECT * FROM sat_hdr WHERE SAH_INV_NO IN ('" + _invoiceno + "')";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection);
            _oDa.SelectCommand = _oCom;
            _oDa.Fill(_emsData, "EMS_SALES_HDR");

            for (I = 0; I <= _emsData.Tables["EMS_SALES_HDR"].Rows.Count - 1; I++)
            {
                #region check and assign header

                _comCode = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_COM"].ToString();

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DIRECT"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DIRECT"] = "N/A";
                _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DIRECT"] = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DIRECT"].ToString().Replace("'", "`");
                _direct = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DIRECT"].ToString();
                if (_direct == "1")
                {
                    _direct = "IN";
                    _uploadType = "POS_INV";
                }
                else
                {
                    _direct = "OUT";
                    _uploadType = "POS_REV";
                }

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_NAME"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_NAME"] = "N/A";
                _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_NAME"] = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_NAME"].ToString().Replace("'", "`");
                _ems_CusName = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_NAME"].ToString();

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD1"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD1"] = "N/A";
                _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD1"] = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD1"].ToString().Replace("'", "`");

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD2"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD2"] = "N/A";
                _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD2"] = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD2"].ToString().Replace("'", "`");
                _ems_CusAddress = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD1"].ToString() + "," + (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_ADD2"].ToString();

                _ems_InvNo = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_INV_NO"].ToString();

                _ems_PC = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_PC"].ToString();
                _locCode = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_PC"].ToString();

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_ANAL_2"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_ANAL_2"] = "0";
                para_TaxInv = 0;
                if (_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_ANAL_2"].ToString() == "1") para_TaxInv = 1;

                if (_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_STUS"].ToString() == "C")
                {
                    para_sale_Status = "Y";
                }
                else if (_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_STUS"].ToString() == "A")
                {
                    para_sale_Status = "N";
                }
                else if (_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_STUS"].ToString() == "D")
                {
                    para_sale_Status = "N";
                }

                //Try to get solution for messe-up Executive Code
                if (_comCode == "AOA" | _comCode == "ABA" | _comCode == "ALL")
                {
                }
                else
                {
                    _RepCode = "N/A";
                }

                if (string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_CD"].ToString())) _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_CD"] = "N/A";
                _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_CD"] = _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_CD"].ToString().Replace("'", "`");
                _ems_CusCode = (string)_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_CUS_CD"].ToString();

                // ConnectionOpen();

                //if (Check_CustCode(_ems_CusCode) == false)
                //{
                //    _ems_CusCode = "CASH";
                //}

                #endregion check and assign header

                #region check and assign items

                _sql = "SELECT * FROM SAT_ITM WHERE SAD_INV_NO = :p_invno";
                _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(new OracleParameter(":p_invno", _ems_InvNo));
                _oDa.SelectCommand = _oCom;
                _oDa.Fill(_emsData, "EMS_SALES_DET");

                #endregion check and assign items

                if (Check_InvoiceNo(_ems_InvNo) == true)
                {
                    continue;
                }
                if (_emsData.Tables["EMS_SALES_DET"].Rows.Count <= 0)
                {
                    continue;
                }

                _InvSeqNo = Get_SCM_Invoice_Seq_No();

                // ConnectionClose();

                //using (TransactionScope tr = new TransactionScope())
                //{
                // ConnectionOpen();

                #region Insert Header

                _sql = "INSERT INTO WHF_INVOICE_HEADER " +
                    " (INVOICE_LINE,INVOICE_TYPE,INVOICE_NO,INVOICE_REF_NO,COMPANY_CODE,PROFIT_CENTER_CODE,CURRENCY_CODE,EXCHANGE_RATE,SALE_EX_CODE,CUSTOMER_CODE, INVOICE_DATE, INVOICE_AMOUNT, INVOICE_TAX_AMOUNT, TOTAL_INVOICE_AMOUNT, CANCEL, CANCEL_USER, DO_STATUS, " +
                    " ENTRY_STATUS, INVOICEING_COMPUTER_IP, SALES_REP, D_ADD1, D_ADD2, DIS_RATE, DIS_AMOUNT, TRANSPORT, OTHER, TOT_DISCOUNT, PRICE_BOOK, " +
                    " PRICE_LEVEL,DOC_TAX_AMOUNT,TMP_INVOICE,OTHER_DOC_NO,IS_TAX,REMARKS,POS_INV_NO,DEDUCTION,SERVICE_CHG,BUYBACK_CHG,POS_INV_TYPE,POS_INV_SUB_TYPE,CASH_DIRECTION,CUSTOMER_ACC_NO,LOC_CODE,CREATE_BY) " +
                    " VALUES(:invline,:uploadtype,:invno,:invrefno,:comcode,:profitcenter,'LKR',1,:repcode,:customer,:invdate,:netsale,:vat, " +
                    " :totalvalue,:salestatus,'N/A','PENDING','PENDING',:terminal,:manager,:custname,:custaddress,0,:discounts,0,:charges, " +
                    " :discounts,:mainpricebook,'N/A',0,0,:otherref,:taxinv,:remarks,:posinvoiceno,:deductions,:servicechg," +
                    " :bbchg,:invtype,:invsubtype, :direct,:accountno,:location,:loguser)";

                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(new OracleParameter(":invline", _InvSeqNo));
                _oCom.Parameters.Add(new OracleParameter(":uploadtype", _uploadType));//ADD CHAMAL 28-02-2013 POS_INV, POS_REV
                _oCom.Parameters.Add(new OracleParameter(":invno", _ems_InvNo));
                _oCom.Parameters.Add(new OracleParameter(":invrefno", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_MAN_REF"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_MAN_REF"].ToString()));
                //_oCom.Parameters.Add(new OracleParameter(":invrefno","0"));
                _oCom.Parameters.Add(new OracleParameter(":comcode", _comCode));
                _oCom.Parameters.Add(new OracleParameter(":profitcenter", _ems_PC.ToUpper()));
                _oCom.Parameters.Add(new OracleParameter(":repcode", _RepCode));
                _oCom.Parameters.Add(new OracleParameter(":customer", _ems_CusCode.ToUpper()));
                _oCom.Parameters.Add(new OracleParameter(":invdate", Convert.ToDateTime(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_DT"].ToString())));
                _oCom.Parameters.Add(new OracleParameter(":netsale", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":vat", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":totalvalue", TotalInvAmt));
                _oCom.Parameters.Add(new OracleParameter(":salestatus", para_sale_Status));
                _oCom.Parameters.Add(new OracleParameter(":terminal", "N/A"));
                _oCom.Parameters.Add(new OracleParameter(":manager", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_MAN_CD"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_MAN_CD"].ToString()));
                _oCom.Parameters.Add(new OracleParameter(":custname", _ems_CusName.ToUpper()));
                _oCom.Parameters.Add(new OracleParameter(":custaddress", _ems_CusName.ToUpper()));
                _oCom.Parameters.Add(new OracleParameter(":discounts", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":charges", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":mainpricebook", "N/A"));
                _oCom.Parameters.Add(new OracleParameter(":otherref", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_REF_DOC"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_REF_DOC"].ToString()));
                _oCom.Parameters.Add(new OracleParameter(":taxinv", para_TaxInv));
                _oCom.Parameters.Add(new OracleParameter(":remarks", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_REMARKS"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_REMARKS"].ToString()));
                _oCom.Parameters.Add(new OracleParameter(":posinvoiceno", _ems_InvNo.ToUpper()));
                _oCom.Parameters.Add(new OracleParameter(":deductions", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":servicechg", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":bbchg", Convert.ToDouble("0")));
                _oCom.Parameters.Add(new OracleParameter(":invtype", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_INV_TP"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_INV_TP"].ToString()));
                _oCom.Parameters.Add(new OracleParameter(":invsubtype", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_INV_SUB_TP"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_INV_SUB_TP"].ToString()));
                _oCom.Parameters.Add(new OracleParameter(":direct", _direct));
                _oCom.Parameters.Add(new OracleParameter(":accountno", string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_ANAL_2"].ToString()) ? "N/A" : _emsData.Tables["EMS_SALES_HDR"].Rows[I]["SAH_ANAL_2"].ToString()));
                //_oCom.Parameters.Add(new OracleParameter(":accountno","N/A"));
                _oCom.Parameters.Add(new OracleParameter(":location", _locCode));
                _oCom.Parameters.Add(new OracleParameter(":loguser", p_LoginUser));
                _oCom.CommandType = CommandType.Text;
                _ref = _oCom.ExecuteNonQuery();

                #endregion Insert Header

                #region Insert Items

                for (J = 0; J <= _emsData.Tables["EMS_SALES_DET"].Rows.Count - 1; J++)
                {
                    if (_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_INV_NO"].ToString() == _ems_InvNo)
                    {
                        Int32 _ItemLine = Convert.ToInt32(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_ITM_LINE"].ToString());
                        string _ItemCode = _emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_ITM_CD"].ToString();

                        double _Qty = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_QTY"].ToString());
                        double _UnitRate = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_UNIT_RT"].ToString());
                        //double _UnitAmt = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_UNIT_AMT"].ToString());
                        double _UnitAmt = Convert.ToDouble(_Qty * _UnitRate); //For solution SCM2 invoice error 08-10-2012
                        double _DisRate = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_DISC_RT"].ToString());
                        double _DisAmt = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_DISC_AMT"].ToString());
                        double _TaxAmt = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_ITM_TAX_AMT"].ToString());
                        double _TotAmt = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_TOT_AMT"].ToString());

                        string _PriceBook = _emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_PBOOK"].ToString();
                        if (string.IsNullOrEmpty(_PriceBook)) _PriceBook = "N/A";
                        string _PriceLevel = _emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_PB_LVL"].ToString();
                        if (string.IsNullOrEmpty(_PriceLevel)) _PriceLevel = "N/A";

                        double _PBSeq = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_SEQ"].ToString());

                        double _PBItemSeq = 0;
                        if (!string.IsNullOrEmpty(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_ITM_SEQ"].ToString())) _PBItemSeq = Convert.ToDouble(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_ITM_SEQ"].ToString());

                        Int32 _WarrPeriod = Convert.ToInt32(_emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_WARR_PERIOD"].ToString());
                        string _WarrRemarks = _emsData.Tables["EMS_SALES_DET"].Rows[J]["SAD_WARR_REMARKS"].ToString();

                        Get_Item_Code_Details(_ItemCode);

                        _sql = "INSERT INTO WHF_INVOICE_DETAILS " +
                        " (INVOICE_LINE, ITEM_LINE_NO, ITEM_CODE, DESCRIPTION, MODEL, QTY, UNIT_RATE, AMOUNT, ITEM_TAX_AMOUNT, DO_STATUS, ENTRY_STATUS, " +
                        " DO_BALANCE, DIS_RATE, DIS_AMOUNT, TOT_AMOUNT, PER_UNITCOST, PRICE_BOOK, PRICE_LEVEL,UOM, STATUS, TMP_DO_ISSUE, TMP_DO_RECEIVE, " +
                        " ENTRY_NO, NEW_ITEM_CODE, NEW_ITEM_DESC, INVOICE_NO, " +
                        " PB_SEQ, PB_ITEM_SEQ_NO, WARA_PERIOD, WARA_REMARKS) " +
                        " VALUES " +
                        " (:invline, :invlineno, :itemcode, :itemdesc, :model, :qty, :unitrate, :unitamt, :taxamt, 'N', 'N', " +
                        " :qty, :disrate, :disamt, :totamt, 0, :pricebook, :pblevel, :uom, 'GOOD', 0, 0, " +
                        " :posinvoiceno, 'N/A', 'N/A', :invno, " +
                        " :pbseq, :pbitmseq, :warrperiod, :warrremarks)";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(new OracleParameter(":invline", _InvSeqNo));
                        _oCom.Parameters.Add(new OracleParameter(":invlineno", _ItemLine));
                        _oCom.Parameters.Add(new OracleParameter(":itemcode", _ItemCode));
                        _oCom.Parameters.Add(new OracleParameter(":itemdesc", itm_Desc));
                        _oCom.Parameters.Add(new OracleParameter(":model", itm_Model));
                        _oCom.Parameters.Add(new OracleParameter(":qty", _Qty));
                        _oCom.Parameters.Add(new OracleParameter(":unitrate", _UnitRate));
                        _oCom.Parameters.Add(new OracleParameter(":unitamt", _UnitAmt));
                        _oCom.Parameters.Add(new OracleParameter(":taxamt", _TaxAmt));
                        _oCom.Parameters.Add(new OracleParameter(":disrate", _DisRate));
                        _oCom.Parameters.Add(new OracleParameter(":disamt", _DisAmt));
                        _oCom.Parameters.Add(new OracleParameter(":totamt", _TotAmt));
                        _oCom.Parameters.Add(new OracleParameter(":pricebook", _PriceBook));
                        _oCom.Parameters.Add(new OracleParameter(":pblevel", _PriceLevel));
                        _oCom.Parameters.Add(new OracleParameter(":uom", itm_Uom));
                        _oCom.Parameters.Add(new OracleParameter(":posinvoiceno", _ems_InvNo));
                        _oCom.Parameters.Add(new OracleParameter(":invno", _ems_InvNo));
                        _oCom.Parameters.Add(new OracleParameter(":pbseq", _PBSeq));
                        _oCom.Parameters.Add(new OracleParameter(":pbitmseq", _PBItemSeq));
                        _oCom.Parameters.Add(new OracleParameter(":warrperiod", _WarrPeriod));
                        _oCom.Parameters.Add(new OracleParameter(":warrremarks", _WarrRemarks));
                        _oCom.CommandType = CommandType.Text;
                        _ref = _oCom.ExecuteNonQuery();
                    }
                }

                #endregion Insert Items

                //ConnectionCloseSCM();
                //    tr.Complete();
                //}
            }

            //ConnectionCloseEMS();
        }

        #endregion upload process

        #region other functions

        public bool Check_InvoiceNo(string Str_InvNo)
        {
            bool functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = false;

            _sql = "SELECT INVOICE_NO FROM WHF_INVOICE_HEADER WHERE POS_INV_NO = :p_invno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_invno", OracleDbType.NVarchar2).Value = Str_InvNo;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = true;
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public bool Check_CustCode(string Str_CustCode)
        {
            bool functionReturnValue = false;
            OracleDataReader Ordsub = default(OracleDataReader);

            functionReturnValue = false;

            _sql = "SELECT COMPANY_CODE, COMPANY_NAME FROM CUSDEC_COMPANY_PROFILE WHERE COMPANY_CODE =:p_custcd";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_custcd", OracleDbType.NVarchar2).Value = Str_CustCode;
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    functionReturnValue = true;
                }
            }
            Ordsub.Close();
            return functionReturnValue;
        }

        public double Get_SCM_Invoice_Seq_No()
        {
            double seqno = 0;
            OracleDataReader Ordsub = default(OracleDataReader);

            _sql = "SELECT INV_SEQ FROM WHF_INVOICE_SEQ WHERE INV_TYPE =:p_tp";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_tp", OracleDbType.NVarchar2).Value = "INV";
            Ordsub = _oCom.ExecuteReader();
            if (Ordsub.HasRows == true)
            {
                while (Ordsub.Read())
                {
                    seqno = Convert.ToDouble(Ordsub["INV_SEQ"].ToString());
                }
            }
            Ordsub.Close();

            _sql = "UPDATE WHF_INVOICE_SEQ SET INV_SEQ = INV_SEQ + 1 WHERE INV_TYPE =:p_tp";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_tp", OracleDbType.NVarchar2).Value = "INV";
            _oCom.CommandType = CommandType.Text;
            _oCom.ExecuteNonQuery();

            return seqno;
        }

        #endregion other functions

        #endregion ** Upload Sales and Reversal **

        #region ** Upload All Inventory (+) Plus Movements **

        public DataSet FillDataSet(string _documentno)
        {
            _oDa = new OracleDataAdapter();
            DataSet _emsData = new DataSet();

            // Get Header
            _sql = "SELECT ITH_SEQ_NO, ITH_COM, ITH_LOC, ITH_DOC_NO, ITH_DOC_YEAR, ITH_DOC_TP, ITH_CATE_TP, ITH_OTH_DOCNO, ITH_DOC_DATE, ITH_OTH_LOC, ITH_ENTRY_NO, ITH_SUB_TP, ITH_ENTRY_TP, ITH_COM, ITH_BUS_ENTITY, ITH_REMARKS, ITH_VEHI_NO, ITH_DIRECT, ITH_CHANNEL, ITH_PC, ITH_DEL_ADD1, ITH_DEL_ADD2 " +
            "FROM INT_HDR " +
            "WHERE ITH_DIRECT = 1 AND  ITH_STUS IN ('A','F') AND ITH_DOC_NO='" + _documentno + "' ";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection);
            _oDa.SelectCommand = _oCom;
            _oDa.Fill(_emsData, "EMS_INT_HDR");

            //Get Serials
            _sql = "SELECT EMS.INT_SER.ITS_BIN AS BIN, EMS.INT_SER.ITS_DOC_NO AS DOCNO, EMS.INT_SER.ITS_ITM_CD AS ITEM, EMS.MST_ITM_STUS.MIS_OLD_CD AS ITEMSTATUS, EMS.INT_SER.ITS_UNIT_COST AS UNITCOST, '1' AS QTY, " +
                "EMS.INT_SER.ITS_SER_1 AS SER1, EMS.INT_SER.ITS_SER_2 AS SER2, EMS.INT_SER.ITS_SER_3 AS SER3, EMS.INT_SER.ITS_WARR_NO AS WARR, EMS.INT_SER.ITS_ORIG_GRNDT AS GRNDATE, EMS.INT_SER.ITS_ORIG_GRNNO AS GRNNO, " +
                "EMS.INT_SER.ITS_SER_ID AS SERID, EMS.INT_SER.ITS_ITM_LINE AS ITM_LINE, EMS.INT_SER.ITS_BATCH_LINE AS BATCH_LINE, EMS.INT_SER.ITS_SER_LINE AS SER_LINE " +
                "FROM EMS.INT_SER INNER JOIN " +
                "EMS.MST_ITM_STUS ON EMS.INT_SER.ITS_ITM_STUS = EMS.MST_ITM_STUS.MIS_CD " +
                "WHERE (EMS.INT_SER.ITS_SEQ_NO IN " +
                "(SELECT ITH_SEQ_NO " +
                "FROM EMS.INT_HDR " +
                "WHERE (ITH_DIRECT = 1) AND (ITH_STUS IN ('A', 'F') AND (ITH_DOC_NO='" + _documentno + "') )))";
            _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection);
            _oDa.SelectCommand = _oCom;
            _oDa.Fill(_emsData, "EMS_INT_SER");

            return _emsData;
        }

        public void upload_MovementsPlus(DataSet _emsDataSet, string _location, DateTime _date, out string _DocNo, string _remarks)
        {
            //ConnectionOpen();
            //_emsSalesDAL.ConnectionOpen();

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
            DataSet _emsData = _emsDataSet;
            OracleCommand _oCom = new OracleCommand();
            string _autoNumber = string.Empty;

            for (I_He = 0; I_He <= _emsData.Tables["EMS_INT_HDR"].Rows.Count - 1; I_He++)
            {
                _autoNumber = _date.Year.ToString() + "+" + Get_System_Auto_Number("ADJ", "ADJ", "00000", _location, true);
                Update_Auto_Number("ADJ", "ADJ", _location, true);

                AOD_EX = false;
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

                    InventoryDAL _Dll = new InventoryDAL();
                    _Dll.ConnectionOpen();
                    para_Ems_SeqNo = "WC" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SEQ_NO"].ToString() + Convert.ToString(_Dll.GetSerialID());
                    _Dll.ConnectionClose();
                    para_Doc_No = _autoNumber;//"(U)" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                    para_Ems_DocNo = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_DOCNO"].ToString();
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

                    #endregion assign header values

                    //ConnectionClose();

                    //using (TransactionScope tr = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                    //{
                    //ConnectionOpen();
                    //_emsSalesDAL.ConnectionOpen();

                    #region Check AOD Out is SCM2 Location

                    if (Str_Doc_Type == "AOD-IN-LOCAL")
                    {
                        _sql = "SELECT LOC_CODE FROM SCM_LOCATION_MASTER WHERE COM_CODE =:p_com AND LOC_CODE =:p_loc AND IS_ONLINE ='SCM2'";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

                    #endregion Check AOD Out is SCM2 Location

                    Insert_Movement_Header(para_Ems_SeqNo, para_Loca, para_Doc_No, para_Mov_Date.Year, Str_Doc_Type, para_OthetDoc_No, para_Mov_Date.Date, para_Other_Loca, para_Doc_No, "APPROVED", "WC", "ADJ+", "N/A", p_LoginUser, para_Ems_DocNo, para_Sub_Type, para_Entry_Type, para_Com, "N/A", "LKR", 1, _remarks, "N/A", "IN", para_Channel, para_Profit_Center, "N/A", "N/A", trDate.Date);

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

                            #endregion assign serial values

                            Insert_Movement_Cost(p_ComCostMethod, para_Ems_SeqNo, Move_C_Line_No, para_Loca, p_LocaType, para_Doc_No, para_ItemCode, para_ItemStatus, para_ItemCost, para_ItemQty, itm_Uom, para_Doc_No, para_Doc_No, p_LoginUser, para_Mov_Date.Date, _ems_itm_line, _ems_batch_line, _ems_ser_line, para_Com, para_ItemCode, 0, itm_Cate_1, itm_Cate_2, itm_Cate_3, itm_Brand, itm_Model, para_ItemSerial, para_ItemChassis, para_ItemWarr, para_Mov_Date.Date, para_Com_Grn_Date.Date, para_Com_Grn_No, true, para_ItemCode, para_Main_MFC, true, para_bin, _ems_ser_id, string.Empty, 0, 0, 0, Str_Doc_Type);
                            //Insert_FIFO(p_ComCostMethod, para_Com, para_Loca, p_LocaType, Str_Doc_Type, para_Doc_No, para_Mov_Date.Date, Move_Fifo_Line_No, para_ItemCode, para_ItemStatus, para_ItemCost, "N/A", "N/A", para_ItemQty, para_ItemQty, para_ItemQty, 0, 0, p_LoginUser, "N/A", itm_Uom, "N/A", "N/A", para_Ems_SeqNo, "LKR", 1, 1, trDate.Date, _ems_itm_line, _ems_batch_line);
                            //Insert_Company_Inventory(para_Com, para_ItemCode, para_ItemStatus, para_ItemQty, para_ItemQty, 0, p_LoginUser);
                            Insert_Location_Inventory(para_Com, para_Loca, para_ItemCode, para_ItemStatus, para_ItemQty, para_ItemQty, 0, p_LoginUser);
                            //Insert_Bin_Inventory_Details(COMPANY_CODE, LOC_CODE, DEF_BIN, para_ItemCode, para_ItemStatus, para_ItemQty, LOGIN_US, para_Pos_Mov_No, para_Mov_Date)
                        }
                    }

                    _sql = "UPDATE INT_HDR SET ITH_ANAL_12 =1, ITH_ANAL_9 =CURRENT_DATE WHERE ITH_DOC_NO =:p_emsdocno";
                    _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Parameters.Add(":p_emsdocno", OracleDbType.NVarchar2).Value = para_Ems_DocNo;
                    _ref = _oCom.ExecuteNonQuery();

                    //    tr.Complete();
                    //}
                }
                //N1:
            }

            _DocNo = _autoNumber;
            _emsData.Tables["EMS_INT_HDR"].Clear();
            _emsData.Tables["EMS_INT_SER"].Clear();
        }

        #endregion ** Upload All Inventory (+) Plus Movements **

        #region Check Balances

        private bool Check_Loca_Balance(string p_LocaCode, string p_ItemCode, string p_ItemStatus, double p_Qty)
        {
            bool _result = true;
            OracleDataReader OrdModel = default(OracleDataReader);
            _sql = "SELECT SUM(QTY_IN_HAND) AS QTY FROM(INV_LOCATION_INVETORY_DETAILS)" +
                " WHERE (LOCATION_CODE = :p_location) AND (ITEM_CODE_GRN = :p_itemcode) AND (STATUS = :p_status)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = p_LocaCode;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = p_ItemCode;
            _oCom.Parameters.Add(":p_status", OracleDbType.NVarchar2).Value = p_ItemStatus;
            OrdModel = _oCom.ExecuteReader();
            if (OrdModel.HasRows == true)
            {
                while (OrdModel.Read())
                {
                    if (string.IsNullOrEmpty(OrdModel["QTY"].ToString()))
                    {
                        _result = false;
                        break;
                    }
                    else
                    {
                        if (Convert.ToDouble(OrdModel["QTY"].ToString()) < p_Qty)
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                _result = false;
            }
            OrdModel.Close();
            return _result;
        }

        #endregion Check Balances

        #region Insert/Update Movement/Inventory Table

        #region Insert Movement Header

        private void Insert_Movement_Header(string Str_Year_Seq_No, string Str_Transaction_Loca, string Str_Doc_No, int Str_Year, string Str_Doc_Type, string Str_Other_Doc_No, DateTime Str_Doc_Date, string Str_Other_Loca, string Str_Entry_No,
           string Str_Doc_Status, string Str_App_By_1, string Str_App_By_2, string Str_App_By_3, string Str_User, string Str_Manual_Ref_No, string Str_Doc_Sub_Type, string Str_Entry_Type, string Str_Company_Code, string Str_Supplier_Code,
           string Str_Currency_Code, double Str_Exchange_Rate, string Str_Remarks, string Str_Vehicle_NO, string Str_Inv_Direction, string Str_Channel_Code, string Str_Cost_Profit_Code, string Str_Del_Add1, string Str_Del_Add2, DateTime TR_DATE)
        {
            _ref = 0;

            if (Str_Other_Doc_No == "") Str_Other_Doc_No = "N/A";

            //Tr Date - Amalgamation Modification 4/4/2011
            //if (TR_DATE.Date == Convert.ToDateTime("31/DEC/9999").Date)
            //{
            //    _sql = " INSERT INTO INV_MOVEMENT_HEADER(YEAR_SEQ_NO,TRANSACTION_LOCATION,DOC_NO,DOC_YEAR,DOC_TYPE,OTHER_DOC_NO,DOC_DATE,OTHER_LOCATION,ENTRY_NO,STATUS,APPROVED_BY_1,APPROVED_BY_2,APPROVED_BY_3,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,MANUAL_REF_NO,AOD_SUB_TYPE,ENTRY_TYPE,COMPANY_CODE,SUPPLIER_CODE,CURRENCY_CODE,EXCHANGE_RATE,REMARKS,VEHICLENO,INV_DIRECTION,CHANNEL_CODE,COST_PROFIT,DEL_ADD1,DEL_ADD2) " +
            //        " VALUES(:p_yearseqno, :p_location, :p_docno, :p_year, :p_doctype, :p_otherdocno, :p_docdate, :p_otherloca, :p_entryno, :p_docstatus, :p_Appby1, :p_Appby2, :p_Appby3, :p_user, CURRENT_DATE , :p_user, CURRENT_DATE , :p_manualrefno, :p_docsubtype, :p_entrytype, :p_company, :p_suppcode, :p_currencycode, :p_exchangerate, :p_remarks, :p_vehicleno, :p_invdirection, :p_channelcode, :p_costprofitcode, :p_deladd1, :p_deladd2)";
            //}
            //else
            //{
            //    _sql = " INSERT INTO INV_MOVEMENT_HEADER(YEAR_SEQ_NO,TRANSACTION_LOCATION,DOC_NO,DOC_YEAR,DOC_TYPE,OTHER_DOC_NO,DOC_DATE,OTHER_LOCATION,ENTRY_NO,STATUS,APPROVED_BY_1,APPROVED_BY_2,APPROVED_BY_3,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,MANUAL_REF_NO,AOD_SUB_TYPE,ENTRY_TYPE,COMPANY_CODE,SUPPLIER_CODE,CURRENCY_CODE,EXCHANGE_RATE,REMARKS,VEHICLENO,INV_DIRECTION,CHANNEL_CODE,COST_PROFIT,DEL_ADD1,DEL_ADD2,TR_DATE)" +
            //        " VALUES(:p_yearseqno, :p_location, :p_docno, :p_year, :p_doctype, :p_otherdocno, :p_docdate, :p_otherloca, :p_entryno, :p_docstatus, :p_Appby1, :p_Appby2, :p_Appby3, :p_user, CURRENT_DATE , :p_user, CURRENT_DATE , :p_manualrefno, :p_docsubtype, :p_entrytype, :p_company, :p_suppcode, :p_currencycode, :p_exchangerate, :p_remarks, :p_vehicleno, :p_invdirection, :p_channelcode, :p_costprofitcode, :p_deladd1, :p_deladd2,:p_trdate)";
            //}

            _sql = " INSERT INTO INV_MOVEMENT_HEADER(YEAR_SEQ_NO,TRANSACTION_LOCATION,DOC_NO,DOC_YEAR,DOC_TYPE,OTHER_DOC_NO,DOC_DATE,OTHER_LOCATION,ENTRY_NO,STATUS,APPROVED_BY_1,APPROVED_BY_2,APPROVED_BY_3,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,MANUAL_REF_NO,AOD_SUB_TYPE,ENTRY_TYPE,COMPANY_CODE,SUPPLIER_CODE,CURRENCY_CODE,EXCHANGE_RATE,REMARKS,VEHICLENO,INV_DIRECTION,CHANNEL_CODE,COST_PROFIT,DEL_ADD1,DEL_ADD2) " +
                   " VALUES(:p_yearseqno, :p_location, :p_docno, :p_year, :p_doctype, :p_otherdocno, :p_docdate, :p_otherloca, :p_entryno, :p_docstatus, :p_Appby1, :p_Appby2, :p_Appby3, :p_user, CURRENT_DATE , :p_user, CURRENT_DATE , :p_manualrefno, :p_docsubtype, :p_entrytype, :p_company, :p_suppcode, :p_currencycode, :p_exchangerate, :p_remarks, :p_vehicleno, :p_invdirection, :p_channelcode, :p_costprofitcode, :p_deladd1, :p_deladd2)";

            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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
            //if (TR_DATE.Date != Convert.ToDateTime("31/12/9999")) _oCom.Parameters.Add(":p_trdate", OracleDbType.Date).Value = TR_DATE.Date;

            //_oCom.Parameters.Add(new OracleParameter(":p_yearseqno", Str_Year_Seq_No));
            //_oCom.Parameters.Add(new OracleParameter(":p_location", Str_Transaction_Loca));
            //_oCom.Parameters.Add(new OracleParameter(":p_docno", Str_Doc_No));
            //_oCom.Parameters.Add(new OracleParameter(":p_year", Str_Year));
            //_oCom.Parameters.Add(new OracleParameter(":p_doctype", Str_Doc_Type));
            //_oCom.Parameters.Add(new OracleParameter(":p_otherdocno", Str_Other_Doc_No));
            //_oCom.Parameters.Add(new OracleParameter(":p_docdate", Str_Doc_Date.Date));
            //_oCom.Parameters.Add(new OracleParameter(":p_otherloca", Str_Other_Loca));
            //_oCom.Parameters.Add(new OracleParameter(":p_entryno", Str_Entry_No));
            //_oCom.Parameters.Add(new OracleParameter(":p_docstatus", Str_Doc_Status));
            //_oCom.Parameters.Add(new OracleParameter(":p_Appby1", Str_App_By_1));
            //_oCom.Parameters.Add(new OracleParameter(":p_Appby2", Str_App_By_2));
            //_oCom.Parameters.Add(new OracleParameter(":p_Appby3", Str_App_By_3));
            //_oCom.Parameters.Add(new OracleParameter(":p_user", Str_User));
            //_oCom.Parameters.Add(new OracleParameter(":p_manualrefno", Str_Manual_Ref_No));
            //_oCom.Parameters.Add(new OracleParameter(":p_docsubtype", Str_Doc_Sub_Type));
            //_oCom.Parameters.Add(new OracleParameter(":p_entrytype", Str_Entry_Type));
            //_oCom.Parameters.Add(new OracleParameter(":p_company", Str_Company_Code));
            //_oCom.Parameters.Add(new OracleParameter(":p_suppcode", Str_Supplier_Code));
            //_oCom.Parameters.Add(new OracleParameter(":p_currencycode", Str_Currency_Code));
            //_oCom.Parameters.Add(new OracleParameter(":p_exchangerate", Str_Exchange_Rate));
            //_oCom.Parameters.Add(new OracleParameter(":p_remarks", Str_Remarks));
            //_oCom.Parameters.Add(new OracleParameter(":p_vehicleno", Str_Vehicle_NO));
            //_oCom.Parameters.Add(new OracleParameter(":p_invdirection", Str_Inv_Direction));
            //_oCom.Parameters.Add(new OracleParameter(":p_channelcode", Str_Channel_Code));
            //_oCom.Parameters.Add(new OracleParameter(":p_costprofitcode", Str_Cost_Profit_Code));
            //_oCom.Parameters.Add(new OracleParameter(":p_deladd1", Str_Del_Add1));
            //_oCom.Parameters.Add(new OracleParameter(":p_deladd2", Str_Del_Add2));
            //if (TR_DATE.Date != Convert.ToDateTime("31/12/9999")) _oCom.Parameters.Add(new OracleParameter(":p_trdate", TR_DATE.Date));
            _ref = _oCom.ExecuteNonQuery();
        }

        #endregion Insert Movement Header

        #region Insert Movement Item

        private void Insert_Movement_Item(string Str_Year_Seq_No, Int16 Str_MItem_Line_No, string Str_Item_Code, string Str_Item_Status, Double Str_Qty, string Str_UOM, string Str_User, string Str_Transaction_Loca, string Str_Doc_No, Double Str_Unit_Price, string Str_Cate_1, string Str_Cate_2, string Str_Cate_3, string Str_Brand_Code, string Str_Model_No)
        {
            _ref = 0;
            _sql = "UPDATE INV_MOVEMENT_ITEM SET QTY = QTY + :p_qty, CURRENT_QTY = CURRENT_QTY + :p_qty " +
                "WHERE YEAR_SEQ_NO = :p_yearseqno AND TRANSACTION_LOCATION = :p_loca AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        #endregion Insert Movement Item

        #region Insert Movement Cost and Movement Serial

        private void Insert_Movement_Cost(string Str_Cost_Method, string Str_Year_Seq_No, Int16 Str_MCost_Line_No, string Str_Transaction_Loca, string Str_Location_Cate,
            string Str_Doc_No, string Str_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, Double Str_Qty, string Str_Uom, string Str_SCM_Doc_IN_No, string Str_Entry_no, string Str_User,
            DateTime Str_Doc_IN_Date, Int16 Str_Doc_Item_Line_No, Int16 Str_Doc_Batch_Line_No, Int16 Str_Doc_Ser_Line_No, string Str_Company, string Str_Tobond_Item_Code, Double Str_Unit_Amt, string Str_Cate_1, string Str_Cate_2, string Str_Cate_3,
            string Str_Brand_Code, string Str_Model_No, string Str_Serial_no, string Str_Chassis_No, string Str_Warr_no, DateTime Str_Doc_Date, DateTime Str_Com_Grn_Date, string Str_Com_Grn_No,
            Boolean Str_Is_IN_Doc, string Str_Item_Code_Original, string Str_MFC_Code, Boolean Str_IsInwardDoc, string Str_Bin, string Str_Ser_ID, string Str_In_Doc_No, Int16 Str_In_Itm_Line, Int16 Str_In_Batch_Line, Int16 Str_In_Ser_Line, string Str_Doc_Type)
        {
            _ref = 0;

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

            #endregion Update FIFO

            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_MOVEMENT_COST(YEAR_SEQ_NO,ITEM_LINE_NO,TRANSACTION_LOCATION,DOC_NO,ITEM_CODE,STATUS,UNIT_COST,QTY,UOM,GRN_NO,ENTRY_NO,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,INV_DATE,INV_LINE_NO,TO_BOND_LINE_NO,COMPANY_CODE,ITEM_CODE_TOBOND,CURRENT_QTY,UNIT_AMOUNT,CATOGARY_1_CODE,CATOGARY_2_CODE,CATOGARY_3_CODE,BRAND_CODE,MODEL_NO,ITEM_CODE_ORIGINAL,UPDATE_COST,UPDATE_EX_COST,C_COST) VALUES " +
                "(:p_yearseqno, :p_lineno, :p_loca, :p_docno, :p_itemcode, :p_itemstatus, :p_unitcost, :p_qty, :p_uom, :p_scmdocinno, :p_entryno, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE , :p_docindate, :p_scminlineno, :p_docitemline, :p_company, :p_tobonditemcode, :p_qty, :p_unitamt, :p_cate1, :p_cate2, :p_cate3, :p_brand, :p_model, :p_itemcodeorig, :p_initmline, :p_inbatchline, :p_docbatchline)";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

                #endregion Insert FIFO

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

        #endregion Insert Movement Cost and Movement Serial

        #region Insert Bin FIFO

        private void SAVE_INVENTORY_ITEM_TO_BIN_DETAILS(string STR_COMPANY_CODE, string STR_LOCATION_CODE, string STR_BIN_LOCATION, string STR_ITEM_CODE, string STR_ITEM_STATUS, double STR_QTY_IN_HAND, double STR_FREE_QTY, double STR_RESERVED_QTY, double STR_ISSUED_QTY, string STR_USER, string STR_DOC_REF_NO, System.DateTime STR_DOC_DATE)
        {
            _ref = 0;

            _sql = "UPDATE INV_BIN_INVENTORY_DETAILS " +
                "SET QTY_IN_HAND = QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_fqty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
                "WHERE COMPANY_CODE =:p_com AND LOCATION_CODE =:p_loc AND BIN_CODE =:p_bin AND ITEM_CODE =:p_item AND ITEM_STATUS =:p_itemstaus AND DOC_REF_NO =:p_docrefno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

            // Added by Nadeeka 07-may-2015 (Issue Fixed)

            _sql = "update inv_bin_inventory_header set qty_in_hand=qty_in_hand  + :p_qty ,    " +
          "  free_qty=free_qty + :p_fqty ,last_modify_by  =:p_user , last_modify_when=CURRENT_DATE where company_code=:p_com " +
            " and location_code=:p_loc and bin_location=:p_bin and item_code=:p_item and item_status=:p_itemstaus "; 
              _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = STR_QTY_IN_HAND;
            _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = STR_FREE_QTY;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = STR_USER;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = STR_COMPANY_CODE;
            _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = STR_LOCATION_CODE;
            _oCom.Parameters.Add(":p_bin", OracleDbType.NVarchar2).Value = STR_BIN_LOCATION;
            _oCom.Parameters.Add(":p_item", OracleDbType.NVarchar2).Value = STR_ITEM_CODE;
            _oCom.Parameters.Add(":p_itemstaus", OracleDbType.NVarchar2).Value = STR_ITEM_STATUS;
             _ref = _oCom.ExecuteNonQuery();
            if (_ref <= 0)
            {
                _sql = "INSERT INTO inv_bin_inventory_header " +
                    " (COMPANY_CODE, LOCATION_CODE, bin_location, ITEM_CODE, ITEM_STATUS, QTY_IN_HAND, FREE_QTY, " +
                    " RESERVED_QTY, ISSUED_QTY, CREATE_BY, CREATE_WHEN, LAST_MODIFY_BY, LAST_MODIFY_WHEN)" +
                    " VALUES(:p_com, :p_loc, :p_bin, :p_item, :p_itemstaus, :p_qty, :p_fqty, :p_rqty, 0, :p_user,CURRENT_DATE , :p_user,CURRENT_DATE )";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = STR_QTY_IN_HAND;
                _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = STR_FREE_QTY;
                _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = STR_RESERVED_QTY;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = STR_USER;
                _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = STR_COMPANY_CODE;
                _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = STR_LOCATION_CODE;
                _oCom.Parameters.Add(":p_bin", OracleDbType.NVarchar2).Value = STR_BIN_LOCATION;
                _oCom.Parameters.Add(":p_item", OracleDbType.NVarchar2).Value = STR_ITEM_CODE;
                _oCom.Parameters.Add(":p_itemstaus", OracleDbType.NVarchar2).Value = STR_ITEM_STATUS;
          
                _ref = _oCom.ExecuteNonQuery();
            }


        }

        #endregion Insert Bin FIFO

        #region Get Movement Cost Line No

        private Int16 Get_Mov_Cost_Line_No(string Str_Year_Seq_No, string Str_Transaction_Loca, string Str_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, string Str_Doc_IN_No, Int16 Str_Doc_IN_Line_No, Int16 Str_Doc_IN_Batch_Line_No)
        {
            _ref = 0;
            OracleDataReader OrdMovCNo;
            Int16 Mov_Item_Line = 0;
            _sql = "SELECT ITEM_LINE_NO FROM INV_MOVEMENT_COST " +
                "WHERE YEAR_SEQ_NO = :p_yearseqno AND TRANSACTION_LOCATION = :p_location AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus AND UNIT_COST = :p_unitcost AND GRN_NO = :p_docinno AND INV_LINE_NO = :p_docinlineno AND TO_BOND_LINE_NO = :p_docinbatchline";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_docinno", OracleDbType.NVarchar2).Value = Str_Doc_IN_No;
            _oCom.Parameters.Add(":p_docinlineno", OracleDbType.Int16).Value = Str_Doc_IN_Line_No;
            _oCom.Parameters.Add(":p_docinbatchline", OracleDbType.Int16).Value = Str_Doc_IN_Batch_Line_No;
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

        #endregion Get Movement Cost Line No

        #region Get SCM In Cost Line No

        private Int16 Get_SCM_IN_Cost_Line_No(string Str_DocNo, string Str_Transaction_Loca, string Str_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, Int16 Str_IN_Item_Line, Int16 Str_IN_Batch_Line)
        {
            _ref = 0;
            //UPDATE_COST     --> IN ITEM LINE
            //UPDATE_EX_COST  --> IN BATCH LINE

            //Inward
            //INV_LINE_NO       --> IN ITEM LINE
            //TO_BOND_LINE_NO   --> IN BATCH LINE

            //------------------------------------------------------------------ 22-09-2012
            //IN/OUT
            //INV_MOVEMENT_COST.TO_BOND_LINE_NO   = INT_BATCH.ITB_ITM_LINE
            //INV_MOVEMENT_COST.C_COST            = INT_BATCH.ITB_BATCH_LINE

            //OUT
            //INV_MOVEMENT_COST.UPDATE_COST       = INT_BATCH.ITB_BASE_ITMLINE
            //INV_MOVEMENT_COST.UPDATE_EX_COST    = INT_BATCH.ITB_BASE_BATCHLINE
            //------------------------------------------------------------------ 22-09-2012

            OracleDataReader OrdMovCNo;
            Int16 Mov_Item_Line = 0;
            //_sql = "SELECT ITEM_LINE_NO FROM INV_MOVEMENT_COST " +
            //    "WHERE TRANSACTION_LOCATION =:p_location AND DOC_NO =:p_doc AND ITEM_CODE =:p_itemcode AND STATUS =:p_itemstatus AND UNIT_COST =:p_unitcost AND " +
            //    "INV_LINE_NO = :p_initmline AND TO_BOND_LINE_NO = :p_inbatchline";

            _sql = "SELECT ITEM_LINE_NO FROM INV_MOVEMENT_COST " +
              "WHERE TRANSACTION_LOCATION =:p_location AND DOC_NO =:p_doc AND ITEM_CODE =:p_itemcode AND STATUS =:p_itemstatus AND UNIT_COST =:p_unitcost AND " +
              "TO_BOND_LINE_NO = :p_initmline AND C_COST = :p_inbatchline";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        #endregion Get SCM In Cost Line No

        #region Insert Serial Inventory

        private void Insert_Item_Serial_Details(string Str_Company_Code, string Str_Location_Code, string Str_Bin_Location, string Str_Item_Code, string Str_Item_Status, string Str_Serial_No, string Str_Warrenty_No, string Str_Availability, Double Str_Qty, string Str_Doc_Ref_No, Int16 Str_Item_Line_No, string Str_User, DateTime Str_Inv_Date, Double Str_Unit_Cost, string Str_Chassis_No, DateTime Str_Com_Grn_Date, string Str_Com_Grn_No, Int16 Str_Updated_Line_No, string Str_MFC_Code, string Str_GRNA_NO)
        {
            _ref = 0;
            _sql = "INSERT INTO INV_ITEM_SERIAL_DETAILS " +
                " (COMPANY_CODE, LOCATION_CODE, BIN_LOCATION, ITEM_CODE, ITEM_STATUS, SERIAL_NO, WARRENTY_NO, AVAILABILITY, QTY, DOC_REF_NO, ITEM_LINE_NO, CREATE_BY, CREATE_WHEN, LAST_MODIFY_BY, LAST_MODIFY_WHEN, INV_DATE, UPDATE_LINE_NO, UNIT_COST, CHASSIS_NO, GRN_DATE, COM_REF_NO, MFC_CODE, GRNA_NO) " +
                " VALUES (:p_comcode, :p_loccode, :p_bincode, :p_itemcode, :p_itemstatus, :p_serno, :p_warrno, :p_availability, :p_qty, :p_docrefno, :p_itemlineno, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_invdate, :p_updatedlineno, :p_unitcost, :p_chassisno, :p_comgrndate, :p_comgrnno, :p_mfc, :p_grna)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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
            _oCom.Parameters.Add(":p_comgrndate", OracleDbType.Date).Value = Str_Inv_Date.Date;
            _oCom.Parameters.Add(":p_comgrnno", OracleDbType.NVarchar2).Value = Str_Com_Grn_No;
            _oCom.Parameters.Add(":p_mfc", OracleDbType.NVarchar2).Value = Str_MFC_Code;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_GRNA_NO;
            _ref = _oCom.ExecuteNonQuery();
        }

        #endregion Insert Serial Inventory

        #region Insert Movement Item Serials

        private void Insert_Movement_Item_Serials(string Str_Year_Seq_No, Int16 Str_MCost_Line_No, string Str_Transaction_Loca, string Str_Doc_No, DateTime Str_Doc_Date, string Str_Item_Code, string Str_Item_Status, Double Str_Qty, string Str_Bin_Code, Double Str_Unit_Cost, string Str_Serial_No, string Str_Chassis_No, string Str_Warranty_No, string Str_Doc_Ref_No, Double Str_Item_Line_No, DateTime Str_Com_In_Date, string Str_Com_In_No, string Str_MFC_Code, Boolean Str_IsInwardDoc, string Str_GRNA_NO, Int16 Str_Batch_Line_No, Int16 Str_Ser_Line_No)
        {
            _ref = 0;
            _sql = "INSERT INTO INV_MOVEMENT_ITEM_SERIALS " +
                " (YEAR_SEQ_NO, TRANSACTION_LOCATION, DOC_NO, DOC_DATE, BIN_LOCA, ITEM_LINE_NO, ITEM_CODE, ITEM_STATUS, SERIAL_SEQ_NO, SERIAL_NO, CHASSIS_NO, WARRENTY_NO, UNIT_PRICE, QTY, DOC_REF_NO, UPDATE_LINE_NO, GRN_DATE, COM_REF_NO, MFC_CODE, GRNA_NO, DOC_LINE_NO) " +
                " VALUES " +
                " (:p_yearseqno, :p_loca, :p_docno, :p_docdate, :p_bin, :p_mcostlineno, :p_itemcode, :p_itemstatus, :p_serialseqno, :p_serno, :p_chassisno, :p_warrno, :p_unitcost, :p_qty, :p_docrefno, :p_updatelineno, :p_comindate, :p_cominno, :p_mfc, :p_grna, :p_doclineno)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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
            _oCom.Parameters.Add(":p_comindate", OracleDbType.Date).Value = Str_Doc_Date.Date;//changed by Prabhath on 10/06/2013
            _oCom.Parameters.Add(":p_cominno", OracleDbType.NVarchar2).Value = Str_Com_In_No;
            _oCom.Parameters.Add(":p_mfc", OracleDbType.NVarchar2).Value = Str_MFC_Code;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_GRNA_NO;
            _oCom.Parameters.Add(":p_doclineno", OracleDbType.Int16).Value = Str_Batch_Line_No;
            _oCom.Parameters.Add(":p_serialseqno", OracleDbType.Int16).Value = Str_Ser_Line_No;
            _ref = _oCom.ExecuteNonQuery();

            //Int16 para_Sub_Ser_Doc_Line = 1;
            //OracleDataReader Ord;
            //_sql = "SELECT * FROM IN_T_Receipt_Serial_Sub WHERE sser_mov_Txn_Ref ='' AND sser_MainItemCode ='' AND sser_MainSerial_1=''";
            //_oCom = new OracleCommand(_sql, oConnection);
            //Ord = _oCom.ExecuteReader();
            //if (Ord.HasRows == true)
            //{
            //    while (Ord.Read())
            //    {
            //        //(Int16)Ord["ITEM_LINE_NO"];
            //        //Insert_Sub_Item_Serials(Str_Year_Seq_No, p_ComCode, p_LocaCode, p_DefBinCode, Str_Doc_No, para_Sub_Ser_Doc_Line, UCase(Trim(_Sqlrd4("sser_ItemCode"))), UCase(Trim(_Sqlrd4("Sub_Item_Status"))), UCase(Trim(_Sqlrd4("sser_ItemType"))), Val(_Sqlrd4("sser_Cost")), Trim(_Sqlrd4("sser_Serial_1")), Trim(_Sqlrd4("sser_Warranty_Period")), Val(_Sqlrd4("sser_Qty")), Move_S_Line_No, UCase(Trim(_Sqlrd4("ser_ItemCode"))), UCase(Trim(Str_Item_Status)), Str_Serial_No, Str_Doc_No, Str_MCost_Line_No, Str_Warranty_No, Trim(_Sqlrd4("ser_MFC")), Str_IsInwardDoc);
            //        para_Sub_Ser_Doc_Line += 1;
            //    }
            //}
            //Ord.Close();

            Move_S_Line_No += 1;
        }

        private void Update_SCM_Warr(string Str_DO_No, DateTime Str_warrantystartdate, Int16 Str_warrantyperiod, string Str_salesorderno, string Str_customercode, string Str_customername, string Str_customeraddressinvoce, string Str_customerphoneno, DateTime Str_podate, string Str_invoiceno, string Str_transaction_location, string Str_warranty_remarks, string Str_itemcode, string Str_serialno, string Str_Reg_No, String Str_Item_Status, double Str_Unit_Price)
        {
            _ref = 0;
            _sql = "UPDATE TEMP_WARA_UPLOAD SET " +
            " warrantystartdate =:warrantystartdate, " +
            " warrantyperiod =:warrantyperiod, " +
            " salesorderno=:salesorderno, " +
            " customercode=:customercode, " +
            " customername=:customername, " +
            " customeraddressinvoce=:customeraddressinvoce, " +
            " customerphoneno=:customerphoneno, " +
            " podate=:podate, " +
            " invoiceno=:invoiceno, " +
            " updatedby='SCM2', " +
            " updateddate=CURRENT_DATE, " +
            " is_upload_wal=0, " +
            " is_upload_dup=0, " +
            " transaction_location=:transaction_location, " +
            " is_pos_upload=0, " +
            " warranty_remarks=:warranty_remarks " +
            " WHERE itemcode =:itemcode AND serialno = :serialno ";

            _sql = "UPDATE TEMP_WARA_UPLOAD SET " +
            " warrantystartdate =:warrantystartdate, " +
            " warrantyperiod =:warrantyperiod, " +
            " salesorderno=:salesorderno, " +
            " customercode=:customercode, " +
            " customername=:customername, " +
            " customeraddressinvoce=:customeraddressinvoce, " +
            " customerphoneno=:customerphoneno, " +
            " podate=:podate, " +
            " invoiceno=:invoiceno, " +
            " updatedby='SCM2', " +
            " updateddate=CURRENT_DATE, " +
            " is_upload_wal=0, " +
            " is_upload_dup=0, " +
            " transaction_location=:transaction_location, " +
            " is_pos_upload=0, " +
            " itemstatus =:itemstatus, " +
            " unitprice =:unitprice, " +
            " vhlregno =:vhlregno, " +
            " warranty_remarks=:warranty_remarks " +
            " WHERE itemcode =:itemcode AND serialno = :serialno ";

            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":warrantystartdate", OracleDbType.Date).Value = Str_warrantystartdate.Date;
            _oCom.Parameters.Add(":warrantyperiod", OracleDbType.Int16).Value = Str_warrantyperiod;
            _oCom.Parameters.Add(":salesorderno", OracleDbType.NVarchar2).Value = Str_salesorderno;
            _oCom.Parameters.Add(":customercode", OracleDbType.NVarchar2).Value = Str_customercode;
            _oCom.Parameters.Add(":customername", OracleDbType.NVarchar2).Value = Str_customername;
            _oCom.Parameters.Add(":customeraddressinvoce", OracleDbType.NVarchar2).Value = Str_customeraddressinvoce;
            _oCom.Parameters.Add(":customerphoneno", OracleDbType.NVarchar2).Value = Str_customerphoneno;
            _oCom.Parameters.Add(":podate", OracleDbType.Date).Value = Str_podate.Date;
            _oCom.Parameters.Add(":invoiceno", OracleDbType.NVarchar2).Value = Str_invoiceno;
            _oCom.Parameters.Add(":transaction_location", OracleDbType.NVarchar2).Value = Str_transaction_location;
            _oCom.Parameters.Add(":warranty_remarks", OracleDbType.NVarchar2).Value = Str_warranty_remarks;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":unitprice", OracleDbType.Double).Value = Str_Unit_Price;
            _oCom.Parameters.Add(":vhlregno", OracleDbType.NVarchar2).Value = Str_Reg_No;
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = Str_itemcode;
            _oCom.Parameters.Add(":serialno", OracleDbType.NVarchar2).Value = Str_serialno;
            _ref = _oCom.ExecuteNonQuery();

            _sql = "UPDATE INR_SERMST SET irsm_loc =:loc, irsm_doc_no =:do_no, irsm_doc_dt =:do_date, irsm_invoice_no =:invc_no, irsm_invoice_dt =:invc_date, " +
            "irsm_warr_start_dt =:do_date, irsm_warr_period =:warr_period, irsm_warr_rem =:warr_remk," +
            "irsm_cust_cd =:cust_code, irsm_cust_prefix =:cust_prefix, irsm_cust_name =:cust_name, irsm_cust_addr =:cust_add, irsm_cust_del_addr =:cust_add," +
            "irsm_cust_tel =:cust_tel, irsm_warr_stus ='Y', irsm_reg_no =:reg_no, irsm_unit_price =:unit_price " +
            "WHERE irsm_itm_cd =:item_code and irsm_ser_1 =:ser_no ";

            _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":loc", OracleDbType.NVarchar2).Value = Str_transaction_location;
            _oCom.Parameters.Add(":do_no", OracleDbType.NVarchar2).Value = Str_DO_No;
            _oCom.Parameters.Add(":do_date", OracleDbType.Date).Value = Str_warrantystartdate.Date;
            _oCom.Parameters.Add(":invc_no", OracleDbType.NVarchar2).Value = Str_invoiceno;
            _oCom.Parameters.Add(":invc_date", OracleDbType.Date).Value = Str_warrantystartdate.Date;
            _oCom.Parameters.Add(":warr_period", OracleDbType.Int16).Value = Str_warrantyperiod;
            _oCom.Parameters.Add(":warr_remk", OracleDbType.NVarchar2).Value = Str_warranty_remarks;

            _oCom.Parameters.Add(":cust_code", OracleDbType.NVarchar2).Value = Str_customercode;
            _oCom.Parameters.Add(":cust_prefix", OracleDbType.NVarchar2).Value = "Mr.";
            _oCom.Parameters.Add(":cust_name", OracleDbType.NVarchar2).Value = Str_customername;
            _oCom.Parameters.Add(":cust_add", OracleDbType.NVarchar2).Value = Str_customeraddressinvoce;
            _oCom.Parameters.Add(":cust_tel", OracleDbType.NVarchar2).Value = Str_customerphoneno;
            _oCom.Parameters.Add(":reg_no", OracleDbType.NVarchar2).Value = Str_Reg_No;
            _oCom.Parameters.Add(":unit_price", OracleDbType.NVarchar2).Value = Str_Unit_Price;
            _oCom.Parameters.Add(":item_code", OracleDbType.NVarchar2).Value = Str_itemcode;
            _oCom.Parameters.Add(":ser_no", OracleDbType.NVarchar2).Value = Str_serialno;
            _ref = _oCom.ExecuteNonQuery();
        }

        private string Get_SCM_Cust_Code(String _EmsCustCode)
        {
            OracleDataAdapter Oadp;
            DataSet Ods;
            string _cust = "CASH";

            _sql = "SELECT COMPANY_CODE FROM CUSDEC_COMPANY_PROFILE WHERE COMPANY_CODE =:cust_code";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":cust_code", OracleDbType.NVarchar2).Value = _EmsCustCode;
            Oadp = new OracleDataAdapter(_oCom);
            Ods = new DataSet();
            Oadp.Fill(Ods, "SCM_CUST");
            Oadp.Dispose();

            DataTableReader Odtr1 = new DataTableReader(Ods.Tables["SCM_CUST"]);
            Ods.Tables["SCM_CUST"].CreateDataReader();
            if (Odtr1.HasRows)
            {
                while (Odtr1.Read())
                {
                    _cust = Odtr1["COMPANY_CODE"].ToString();
                }
            }
            Odtr1.Close(); Odtr1.Dispose();

            return _cust;
        }

        private string Get_Vehi_Reg_no(string _itemcode, string _engno)
        {
            OracleDataAdapter Oadp;
            DataSet Ods;
            string _regno = "N/A";

            _sql = "SELECT SVRT_VEH_REG_NO, SRVT_ITM_CD, SVRT_ENGINE FROM SAT_VEH_REG_TXN WHERE  NOT (SVRT_VEH_REG_NO IS NULL) AND " +
            "SRVT_ITM_CD =:itemcode AND SVRT_ENGINE =:engno";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _itemcode;
            _oCom.Parameters.Add(":engno", OracleDbType.NVarchar2).Value = _engno;
            Oadp = new OracleDataAdapter(_oCom);
            Ods = new DataSet();
            Oadp.Fill(Ods, "VEH_REG");
            Oadp.Dispose();

            DataTableReader Odtr1 = new DataTableReader(Ods.Tables["VEH_REG"]);
            Ods.Tables["VEH_REG"].CreateDataReader();
            if (Odtr1.HasRows)
            {
                while (Odtr1.Read())
                {
                    _regno = Odtr1["SVRT_VEH_REG_NO"].ToString();
                }
            }
            Odtr1.Close(); Odtr1.Dispose();

            return _regno;
        }

        #endregion Insert Movement Item Serials

        #region Insert FIFO

        private void Insert_FIFO(string Str_Cost_Method, string Str_Company_Code, string Str_Location_Code, string Str_Location_Cate, string Str_Doc_Type, string Str_Doc_Ref_No, DateTime Str_Inv_Date, Int16 Str_MCost_Line_No, string Str_GRN_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, string Str_Tobond_No, string Str_Tobond_Item_Code, Double Str_Tobond_Qty, Double Str_Qty_In_Hand, Double Str_Free_Qty, Double Str_Reserved_Qty, Double Str_Issued_Qty, string Str_User, string Str_PI_No, string Str_Uom, string Str_SI_No, string Str_LC_No, string Str_Entry_No, string Str_Currency_Code, Double Str_Exchange_Rate, Int16 Str_Latest_Cost_Pick, DateTime tr_date, Int16 Str_itm_line, Int16 Str_batch_line)
        {
            _ref = 0;

            //_sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND= QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_qty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
            //" WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND DOC_REF_NO = :p_docrefno AND  ITEM_CODE_GRN = :p_itemcode AND  STATUS = :p_itemstatus AND UNIT_COST = :p_unitcost " +
            //"AND LINE_NO = :p_itmline AND GRN_LINE_NO = :p_batchline";

            _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND= QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_qty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
           " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND DOC_REF_NO = :p_docrefno AND  ITEM_CODE_GRN = :p_itemcode AND  STATUS = :p_itemstatus AND UNIT_COST = :p_unitcost " +
           "AND GRN_LINE_NO = :p_itmline AND TOBOND_LINE_NO = :p_batchline";

            //GRN_LINE_NO       = ITEM LINE
            //TOBOND_LINE_NO    = BATCH LINE
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty_In_Hand;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_Ref_No;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_GRN_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_itmline", OracleDbType.Int16).Value = Str_itm_line;
            _oCom.Parameters.Add(":p_batchline", OracleDbType.Int16).Value = Str_batch_line;
            _ref = _oCom.ExecuteNonQuery();
            if (_ref <= 0)
            {
                //Tr Date - Amalgamation Modification 4/4/2011
                if (tr_date.Date == Convert.ToDateTime("31/12/9999"))
                {
                    _sql = "INSERT INTO INV_LOCATION_INVETORY_DETAILS(COMPANY_CODE,LOCATION_CODE,DOC_TYPE,DOC_REF_NO,INV_DATE,LINE_NO,ITEM_CODE_GRN,STATUS,GRN_LINE_NO,UNIT_COST,TOBOND_NO,ITEM_CODE_TOBOND,TOBOND_LINE_NO,QTY_TOBOND,QTY_IN_HAND,FREE_QTY,RESERVED_QTY,ISSUED_QTY,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,PI_NO,UOM,TO_BOND_UNIT_COST,SI_NO,LC_NO,ENTRY_NO,QTY_TOBOND_ACTUAL,CURRENCY_CODE,EXCHANGE_RATE,LATEST_COST_PICKING) VALUES " +
                    "(:p_company, :p_location, :p_doctype, :p_docrefno, :p_invdate, :p_movlineno, :p_itemcode, :p_itemstatus, :p_itmline, :p_unitcost, :p_tobondno, :p_tobitemcode, :p_batchline, :p_tobqty, :p_qty, :p_fqty, :p_rqty, :p_iqty, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_pino, :p_uom, :p_unitcost, :p_sino, :p_lcno, :p_entryno, :p_qty, :p_currencycode, :p_exchangerate, :p_latestcostpick)";
                }
                else
                {
                    _sql = "INSERT INTO INV_LOCATION_INVETORY_DETAILS(COMPANY_CODE,LOCATION_CODE,DOC_TYPE,DOC_REF_NO,INV_DATE,LINE_NO,ITEM_CODE_GRN,STATUS,GRN_LINE_NO,UNIT_COST,TOBOND_NO,ITEM_CODE_TOBOND,TOBOND_LINE_NO,QTY_TOBOND,QTY_IN_HAND,FREE_QTY,RESERVED_QTY,ISSUED_QTY,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,PI_NO,UOM,TO_BOND_UNIT_COST,SI_NO,LC_NO,ENTRY_NO,QTY_TOBOND_ACTUAL,CURRENCY_CODE,EXCHANGE_RATE,LATEST_COST_PICKING,TR_DATE) VALUES " +
                    "(:p_company, :p_location, :p_doctype, :p_docrefno, :p_invdate, :p_movlineno, :p_itemcode, :p_itemstatus, :p_itmline, :p_unitcost, :p_tobondno, :p_tobitemcode, :p_batchline, :p_tobqty, :p_qty, :p_fqty, :p_rqty, :p_iqty, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_pino, :p_uom, :p_unitcost, :p_sino, :p_lcno, :p_entryno, :p_qty, :p_currencycode, :p_exchangerate, :p_latestcostpick,:p_trdate)";
                }

                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
                _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Location_Code;
                _oCom.Parameters.Add(":p_doctype", OracleDbType.NVarchar2).Value = Str_Doc_Type;
                _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_Ref_No;
                _oCom.Parameters.Add(":p_invdate", OracleDbType.Date).Value = Str_Inv_Date.Date;
                _oCom.Parameters.Add(":p_movlineno", OracleDbType.Int16).Value = Str_MCost_Line_No;
                _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_GRN_Item_Code;
                _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
                _oCom.Parameters.Add(":p_tobondno", OracleDbType.NVarchar2).Value = Str_Tobond_No;
                _oCom.Parameters.Add(":p_tobitemcode", OracleDbType.NVarchar2).Value = Str_Tobond_Item_Code;
                _oCom.Parameters.Add(":p_tobqty", OracleDbType.Double).Value = Str_Tobond_Qty;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty_In_Hand;
                _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = Str_Free_Qty;
                _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = Str_Reserved_Qty;
                _oCom.Parameters.Add(":p_iqty", OracleDbType.Double).Value = Str_Issued_Qty;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                _oCom.Parameters.Add(":p_pino", OracleDbType.NVarchar2).Value = Str_PI_No;
                _oCom.Parameters.Add(":p_uom", OracleDbType.NVarchar2).Value = Str_Uom;
                _oCom.Parameters.Add(":p_sino", OracleDbType.NVarchar2).Value = Str_SI_No;
                _oCom.Parameters.Add(":p_lcno", OracleDbType.NVarchar2).Value = Str_LC_No;
                _oCom.Parameters.Add(":p_entryno", OracleDbType.NVarchar2).Value = Str_Entry_No;
                _oCom.Parameters.Add(":p_currencycode", OracleDbType.NVarchar2).Value = Str_Currency_Code;
                _oCom.Parameters.Add(":p_exchangerate", OracleDbType.Double).Value = Str_Exchange_Rate;
                _oCom.Parameters.Add(":p_latestcostpick", OracleDbType.Int16).Value = Str_Latest_Cost_Pick;
                _oCom.Parameters.Add(":p_itmline", OracleDbType.Int16).Value = Str_itm_line;
                _oCom.Parameters.Add(":p_batchline", OracleDbType.Int16).Value = Str_batch_line;
                if (tr_date.Date != Convert.ToDateTime("31/12/9999")) _oCom.Parameters.Add(":p_trdate", OracleDbType.Date).Value = tr_date.Date;
                _ref = _oCom.ExecuteNonQuery();
            }

            //_ref = 0;
            //if (Str_Cost_Method == "AVG" || Str_Is_Avg_Cost_Doc == true)
            //{
            //    _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET UNIT_COST = :p_unitcost, LAST_MODIFY_BY = :p_user,LAST_MODIFY_WHEN = CURRENT_DATE " +
            //    " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE IN (SELECT LOC_CODE FROM SCM_LOCATION_MASTER WHERE COM_CODE = :p_company AND CATEGORY_CODE = :p_locationcate) " +
            //    " AND ITEM_CODE_GRN = :p_itemcode AND STATUS = :p_itemstatus AND QTY_IN_HAND >0";

            //    _oCom = new OracleCommand(_sql, oConnection);
            //    _oCom.Parameters.Add(new OracleParameter(":p_company", Str_Company_Code));
            //    _oCom.Parameters.Add(new OracleParameter(":p_unitcost", Str_Unit_Cost));
            //    _oCom.Parameters.Add(new OracleParameter(":p_user", Str_User));
            //    _oCom.Parameters.Add(new OracleParameter(":p_locationcate", Str_Location_Cate));
            //    _oCom.Parameters.Add(new OracleParameter(":p_itemcode", Str_GRN_Item_Code));
            //    _oCom.Parameters.Add(new OracleParameter(":p_itemstatus", Str_Item_Status));
            //    _ref = _oCom.ExecuteNonQuery();
            //}
        }

        #endregion Insert FIFO

        #region Update FIFO (None-Serial)

        private Boolean Update_Fifo_With_Bin(string _SeqNo, Int16 _ItemLineNo, string _TrLocaCode, string _DocNo, string _ItemCode, string _ItemStatus, Double _Qty, string _Uom, string _User, string _Cate1, string _Cate2, string _Cate3, string _BrandCode, string _ModelNo, string _ComCode, string _ReqType, Double _ReqQty, string _EntryNo, string _ResNo, Int16 _ResLineNo, string _BinCode, DateTime _DocDate, string _SerType, string _JobNo, string _InvDocRefNo, Int16 _InvDocRefLineNo, Boolean _ChkJobNoBase, Boolean _ChkDocRefLineNoBase)
        {
            _ref = 0;
            Double _UpdateQty = 0;
            //Boolean _chkNoErr = false;
            OracleDataReader _OrdSub;

            _UpdateQty = _Qty;

            if (_ChkJobNoBase == true)
            {
                _sql = "SELECT COMPANY_CODE, LOCATION_CODE, BIN_CODE, INV_DATE, DOC_REF_NO, ITEM_CODE, ITEM_STATUS, QTY_IN_HAND " +
                    " FROM INV_BIN_INVENTORY_DETAILS WHERE LOCATION_CODE = :trloccode AND BIN_CODE = :bincode AND ITEM_CODE = :itemcode AND ITEM_STATUS = :itemstatus AND PROJECT_NO = :jobno AND QTY_IN_HAND > 0 " +
                    " ORDER BY INV_DATE, DOC_REF_NO ";
            }
            else if (_ChkDocRefLineNoBase == true)
            {
                _sql = "SELECT COMPANY_CODE, LOCATION_CODE, BIN_CODE, INV_DATE, DOC_REF_NO, ITEM_CODE, ITEM_STATUS, QTY_IN_HAND " +
                    " FROM INV_BIN_INVENTORY_DETAILS WHERE LOCATION_CODE = :trloccode AND BIN_CODE = :bincode AND ITEM_CODE = :itemcode AND ITEM_STATUS = :itemstatus AND DOC_REF_NO = :docrefno AND QTY_IN_HAND > 0 " +
                    " ORDER BY INV_DATE, DOC_REF_NO ";
            }
            else
            {
                _sql = "SELECT COMPANY_CODE, LOCATION_CODE, BIN_CODE, INV_DATE, DOC_REF_NO, ITEM_CODE, ITEM_STATUS, QTY_IN_HAND " +
                    " FROM INV_BIN_INVENTORY_DETAILS WHERE LOCATION_CODE = :trloccode AND BIN_CODE = :bincode AND ITEM_CODE = :itemcode AND ITEM_STATUS = :itemstatus AND QTY_IN_HAND > 0 " +
                    " ORDER BY INV_DATE, DOC_REF_NO ";
            }

            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
            _oCom.Parameters.Add(":trloccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
            _oCom.Parameters.Add(":bincode", OracleDbType.NVarchar2).Value = _BinCode;

            if (_ChkJobNoBase == true)
            {
                _oCom.Parameters.Add(":jobno", OracleDbType.NVarchar2).Value = _JobNo;
            }
            else if (_ChkDocRefLineNoBase == true)
            {
                _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _InvDocRefNo;
            }
            _OrdSub = _oCom.ExecuteReader();
            if (_OrdSub.HasRows == true)
            {
                while (_OrdSub.Read())
                {
                    _UpdateQty = Convert.ToDouble(_OrdSub["QTY_IN_HAND"].ToString());

                    if (_DocDate.Date < Convert.ToDateTime(_OrdSub["INV_DATE"]).Date)
                    {
                        _OrdSub.Close();
                        //MsgBox("The Inward document " + _OrdSub["DOC_REF_NO"] + " date " + Convert.ToDateTime(_OrdSub["INV_DATE"]).Date + " equal or grater than to a this Outward document " + _DocDate.Date + " date!", MsgBoxStyle.Exclamation, "Type Miss Match" + " ( Item Code : " + _ItemCode + " )");
                        return false;
                    }

                    if (_UpdateQty != _Qty)
                    {
                        _sql = "UPDATE INV_BIN_INVENTORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY = :loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
                        " WHERE COMPANY_CODE = :comcode AND LOCATION_CODE = :loccode AND BIN_CODE = :bincode AND ITEM_CODE = :itemcode AND ITEM_STATUS = :itemstatus AND DOC_REF_NO = :docrefno ";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(":comcode", OracleDbType.NVarchar2).Value = _ComCode;
                        _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
                        _oCom.Parameters.Add(":bincode", OracleDbType.NVarchar2).Value = _BinCode;
                        _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
                        _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
                        _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _OrdSub["DOC_REF_NO"];
                        _oCom.Parameters.Add(":qty", OracleDbType.NVarchar2).Value = _UpdateQty;
                        _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = _User;
                        _ref = _oCom.ExecuteNonQuery();

                        Update_FIFO(_SeqNo, _ItemLineNo, _TrLocaCode, _DocNo, _ItemCode, _ItemStatus, _UpdateQty, _Uom, _User, _Cate1, _Cate2, _Cate3, _BrandCode, _ModelNo, _ComCode, _ReqType, _ReqQty, _EntryNo, _ResNo, _ResLineNo, (string)_OrdSub["DOC_REF_NO"], _DocDate.Date, _BinCode, _SerType, _JobNo, _InvDocRefLineNo, _ChkJobNoBase, _ChkDocRefLineNoBase);
                        _Qty = _Qty - _UpdateQty;
                    }
                    else
                    {
                        _sql = "UPDATE INV_BIN_INVENTORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY = :loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
                        " WHERE COMPANY_CODE = :comcode AND LOCATION_CODE = :loccode AND BIN_CODE = :bincode AND ITEM_CODE = :itemcode AND ITEM_STATUS = :itemstatus AND DOC_REF_NO = :docrefno ";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(":comcode", OracleDbType.NVarchar2).Value = _ComCode;
                        _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
                        _oCom.Parameters.Add(":bincode", OracleDbType.NVarchar2).Value = _BinCode;
                        _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
                        _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
                        _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _OrdSub["DOC_REF_NO"];
                        _oCom.Parameters.Add(":qty", OracleDbType.NVarchar2).Value = _UpdateQty;
                        _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = _User;
                        _ref = _oCom.ExecuteNonQuery();

                        Update_FIFO(_SeqNo, _ItemLineNo, _TrLocaCode, _DocNo, _ItemCode, _ItemStatus, _Qty, _Uom, _User, _Cate1, _Cate2, _Cate3, _BrandCode, _ModelNo, _ComCode, _ReqType, _ReqQty, _EntryNo, _ResNo, _ResLineNo, (string)_OrdSub["DOC_REF_NO"], _DocDate.Date, _BinCode, _SerType, _JobNo, _InvDocRefLineNo, _ChkJobNoBase, _ChkDocRefLineNoBase);
                        _Qty = 0;
                    }

                    if (_Qty != 0)
                    {
                        goto A1;
                    }
                }
            }

        A1:
            _OrdSub.Close();
            return true;
        }

        private void Update_FIFO(string _SeqNo, Int16 _ItemLineNo, string _TrLocaCode, string _DocNo, string _ItemCode, string _ItemStatus, Double _Qty, string _Uom, string _User, string _Cate1, string _Cate2, string _Cate3, string _BrandCode, string _ModelNo, string _ComCode, string _ReqType, Double _ReqQty, string _EntryNo, string _ResNo, Int16 _ResLineNo, string _InvDocRefNo, DateTime _DocDate, string _BinCode, string _SerType, string _JobNo, Int16 _InvDocRefLineNo, Boolean _ChkJobNoBase, Boolean _ChkDocRefLineNoBase)
        {
            OracleDataReader _OrdSub;
            _ref = 0;

            string _TobondNo = "";
            DateTime _DocRefDate;
            Double _ItemQty = 0;
            Double _UnitCost = 0;

            _sql = "SELECT COMPANY_CODE, LOCATION_CODE, INV_DATE, TOBOND_NO, DOC_REF_NO, LINE_NO, ITEM_CODE_GRN, STATUS, UNIT_COST, QTY_IN_HAND " +
            " FROM INV_LOCATION_INVETORY_DETAILS WHERE ITEM_CODE_GRN =:itemcode AND STATUS = :itemstatus AND " +
            " LOCATION_CODE =:trloccode AND DOC_REF_NO =:docrefno AND QTY_IN_HAND > 0 " +
            " ORDER BY INV_DATE, DOC_REF_NO, LINE_NO";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
            _oCom.Parameters.Add(":trloccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
            _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _InvDocRefNo;
            _OrdSub = _oCom.ExecuteReader();
            if (_OrdSub.HasRows == true)
            {
                while (_OrdSub.Read())
                {
                    _InvDocRefNo = (string)_OrdSub["DOC_REF_NO"];
                    //ITEM_CODE = Trim(_OrdSub("ITEM_CODE_GRN"))
                    _InvDocRefLineNo = (Int16)_OrdSub["LINE_NO"];
                    //LOCATION_CODE = Trim(_OrdSub("LOCATION_CODE"))
                    _TobondNo = (string)_OrdSub["TOBOND_NO"];
                    _DocRefDate = Convert.ToDateTime(_OrdSub["INV_DATE"]).Date;
                    _ItemQty = (double)_OrdSub["QTY_IN_HAND"];
                    //STATUS = Trim(_OrdSub("STATUS"))
                    _UnitCost = (double)_OrdSub["UNIT_COST"];

                    if (_ItemQty != _Qty)
                    {
                        _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY =:loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
                        " WHERE DOC_REF_NO =:docrefno AND LINE_NO =:docreflineno AND ITEM_CODE_GRN =:itemcode AND LOCATION_CODE =:loccode";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = _ItemQty;
                        _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = _User;
                        _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _InvDocRefNo;
                        _oCom.Parameters.Add(":docreflineno", OracleDbType.Int16).Value = _InvDocRefLineNo;
                        _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
                        _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
                        _oCom.CommandType = CommandType.Text;
                        _ref = _oCom.ExecuteNonQuery();

                        Save_Mov_Cost_Items(_SeqNo, _ItemLineNo, _TrLocaCode, _DocNo, _ItemCode, _ItemStatus, _Cate1, _Cate2, _Cate3, _BrandCode, _ModelNo, _UnitCost, _ItemQty, _Uom, _User, _ComCode, _EntryNo, _DocRefDate.Date, _InvDocRefLineNo, _InvDocRefNo, _TobondNo, _ResNo, _ResLineNo, _DocDate.Date, _BinCode, _SerType);

                        _Qty = _Qty - _ItemQty;
                    }
                    else
                    {
                        _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY =:loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
                        " WHERE DOC_REF_NO =:docrefno AND LINE_NO =:docreflineno AND ITEM_CODE_GRN =:itemcode AND LOCATION_CODE =:loccode";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = _Qty;
                        _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = _User;
                        _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = _InvDocRefNo;
                        _oCom.Parameters.Add(":docreflineno", OracleDbType.Int16).Value = _InvDocRefLineNo;
                        _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
                        _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = _TrLocaCode;
                        _oCom.CommandType = CommandType.Text;
                        _ref = _oCom.ExecuteNonQuery();

                        Save_Mov_Cost_Items(_SeqNo, M_Cost_Line, _TrLocaCode, _DocNo, _ItemCode, _ItemStatus, _Cate1, _Cate2, _Cate3, _BrandCode, _ModelNo, _UnitCost, _Qty, _Uom, _User, _ComCode, _EntryNo, _DocRefDate.Date, _InvDocRefLineNo, _InvDocRefNo, _TobondNo, _ResNo, _ResLineNo, _DocDate.Date, _BinCode, _SerType);

                        _Qty = 0;
                    }

                    if (_Qty != 0)
                    {
                        _ItemLineNo += 1;
                        goto A1;
                    }
                    _ItemLineNo += 1;
                }
            A1:
                if (_Qty > 0)
                {
                    return;
                }
            }
            _OrdSub.Close();
        }

        #endregion Update FIFO (None-Serial)

        #region save move cost

        //Int16 M_Item_Line = 0;
        private Int16 M_Cost_Line = 0;

        private Int16 M_Serial_Line = 0;

        private void Save_Mov_Cost_Items(string _SeqNo, Int16 _ItemLineNo, string _TrLoca, string _DocNo, string _ItemCode, string _ItemStatus, string _Cate1, string _Cate2, string _Cate3, string _Brand,
            string _Model, double _UnitCost, double _Qty, string _Uom, string _User, string _ComCode, string _JobNo, DateTime _InvDate, Int16 _InvLineNo, string _DocRefNo,
            string _TobondNo, string _ResNo, Int16 _ResLineNo, DateTime _AodDate, string _BinCode, string _SerType)
        {
            _ref = 0;
            int k = 0;

            _sql = "UPDATE INV_MOVEMENT_COST SET QTY = QTY + :qty, CURRENT_QTY = CURRENT_QTY + :qty WHERE YEAR_SEQ_NO =:seqno AND ITEM_CODE =:itemcode AND " + " STATUS =:itemstatus AND UNIT_COST =:unitcost AND GRN_NO =:grnno AND INV_LINE_NO =:invlineno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = _Qty;
            _oCom.Parameters.Add(":seqno", OracleDbType.NVarchar2).Value = _SeqNo;
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
            _oCom.Parameters.Add(":unitcost", OracleDbType.Double).Value = _UnitCost;
            _oCom.Parameters.Add(":grnno", OracleDbType.NVarchar2).Value = _DocRefNo;
            _oCom.Parameters.Add(":invlineno", OracleDbType.Int16).Value = _InvLineNo;
            _ref = _oCom.ExecuteNonQuery();

            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_MOVEMENT_COST(YEAR_SEQ_NO, ITEM_LINE_NO, " + " TRANSACTION_LOCATION,DOC_NO,ITEM_CODE, STATUS,CATOGARY_1_CODE,CATOGARY_2_CODE,CATOGARY_3_CODE,BRAND_CODE,MODEL_NO, " + " UNIT_COST, QTY,UOM, CREATE_BY,CREATE_WHEN, LAST_MODIFY_BY,LAST_MODIFY_WHEN,CURRENT_QTY,COMPANY_CODE,ENTRY_NO,INV_DATE,INV_LINE_NO,GRN_NO,TOBOND_NO,RES_NO,RES_LINE_NO) " + " VALUES (:seqno, :mcostlineno, :locacode, :docno, :itemcode, :itemstatus, :cate1, :cate2, :cate3, :brand, :model, " + " :unitcost, :qty, :uom, :loginuser, CURRENT_DATE, :loginuser, CURRENT_DATE, :qty, :comcode, :entryno, :invdate, :invlineno, " + " :grnno, :tobondno, :resno, :reslineno)";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":seqno", OracleDbType.NVarchar2).Value = _SeqNo;
                _oCom.Parameters.Add(":mcostlineno", OracleDbType.Int16).Value = M_Cost_Line;
                _oCom.Parameters.Add(":locacode", OracleDbType.NVarchar2).Value = _TrLoca;
                _oCom.Parameters.Add(":docno", OracleDbType.NVarchar2).Value = _DocNo;
                _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
                _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = _ItemStatus;
                _oCom.Parameters.Add(":cate1", OracleDbType.NVarchar2).Value = _Cate1;
                _oCom.Parameters.Add(":cate2", OracleDbType.NVarchar2).Value = _Cate2;
                _oCom.Parameters.Add(":cate3", OracleDbType.NVarchar2).Value = _Cate3;
                _oCom.Parameters.Add(":brand", OracleDbType.NVarchar2).Value = _Brand;
                _oCom.Parameters.Add(":model", OracleDbType.NVarchar2).Value = _Model;
                _oCom.Parameters.Add(":unitcost", OracleDbType.Double).Value = _UnitCost;
                _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = _Qty;
                _oCom.Parameters.Add(":uom", OracleDbType.NVarchar2).Value = _Uom;
                _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = _User;
                _oCom.Parameters.Add(":comcode", OracleDbType.NVarchar2).Value = _ComCode;
                _oCom.Parameters.Add(":entryno", OracleDbType.NVarchar2).Value = _JobNo;
                _oCom.Parameters.Add(":invdate", OracleDbType.Date).Value = _InvDate.Date;
                _oCom.Parameters.Add(":invlineno", OracleDbType.Int16).Value = _InvLineNo;
                _oCom.Parameters.Add(":grnno", OracleDbType.NVarchar2).Value = _DocRefNo;
                _oCom.Parameters.Add(":tobondno", OracleDbType.NVarchar2).Value = _TobondNo;
                _oCom.Parameters.Add(":resno", OracleDbType.NVarchar2).Value = _ResNo;
                _oCom.Parameters.Add(":reslineno", OracleDbType.Int16).Value = _ResLineNo;
                _ref = _oCom.ExecuteNonQuery();

                if (_SerType == "NO")
                {
                    if (Allow_Decimal(_ItemCode) == true)
                    {
                        Save_Mov_Item_Serials(_SeqNo, _ItemLineNo, _TrLoca, "N/A", _AodDate.Date, _ItemCode, _ItemStatus, _Qty, _BinCode, _UnitCost,
                        "N/A", "N/A", "N/A", _AodDate.Date, "N/A", M_Cost_Line, _DocRefNo);
                    }
                    else
                    {
                        for (k = 1; k <= _Qty; k++)
                        {
                            Save_Mov_Item_Serials(_SeqNo, _ItemLineNo, _TrLoca, "N/A", _AodDate.Date, _ItemCode, _ItemStatus, 1, _BinCode, _UnitCost,
                            "N/A", "N/A", "N/A", _AodDate.Date, "N/A", M_Cost_Line, _DocRefNo);
                        }
                    }
                }

                M_Cost_Line += 1;
            }
            else
            {
                if (_SerType == "NO")
                {
                    //Error check this
                    Int16 _M_Cur_Item_Line = Get_Mov_Cost_Line_No(_SeqNo, _TrLoca, _ItemCode, _ItemStatus, _UnitCost, _DocRefNo, _ItemLineNo, 0);

                    if (Allow_Decimal(_ItemCode) == true)
                    {
                        Save_Mov_Item_Serials(_SeqNo, _ItemLineNo, _TrLoca, "N/A", _AodDate.Date, _ItemCode, _ItemStatus, _Qty, _BinCode, _UnitCost,
                        "N/A", "N/A", "N/A", _AodDate.Date, "N/A", _M_Cur_Item_Line, _DocRefNo);
                    }
                    else
                    {
                        for (k = 1; k <= _Qty; k++)
                        {
                            Save_Mov_Item_Serials(_SeqNo, _ItemLineNo, _TrLoca, "N/A", _AodDate.Date, _ItemCode, _ItemStatus, 1, _BinCode, _UnitCost,
                            "N/A", "N/A", "N/A", _AodDate.Date, "N/A", _M_Cur_Item_Line, _DocRefNo);
                        }
                    }
                }
            }
        }

        #endregion save move cost

        #region save mov item serials

        private void Save_Mov_Item_Serials(string STR_YEAR_SEQ_NO, int STR_ITEM_LINE_NO, string STR_TRANSACTION_LOCATION, string STR_DOC_NO, DateTime STR_DOC_DATE, string STR_ITEM_CODE, string STR_STATUS, double STR_QTY, string STR_BIN_LOCA, double STR_UNIT_COST,
    string STR_SERIAL_NO, string STR_CHASSIS_NO, string STR_WARRENTY_NO, System.DateTime STR_COM_GRN_DATE, string STR_COM_GRN_NO, int STR_UPDATE_LINE_NO = 0, string STR_DOC_REF_NO = "N/A", string STR_GRAN_NO = "N/A", double STR_ITEM_CBM = 0, string STR_WMS_COM_CODE = "N/A",
    string STR_WMS_WH_CODE = "N/A", string STR_WMS_BIN_CODE = "N/A", string STR_MFC_CODE = "N/A")
        {
            _ref = 0;
            _sql = "INSERT INTO INV_MOVEMENT_ITEM_SERIALS " +
                " (YEAR_SEQ_NO,TRANSACTION_LOCATION,DOC_NO,DOC_DATE,BIN_LOCA,ITEM_LINE_NO,ITEM_CODE,ITEM_STATUS,SERIAL_SEQ_NO,SERIAL_NO, " + " WARRENTY_NO,UNIT_PRICE,QTY,UPDATE_LINE_NO,DOC_REF_NO,DOC_LINE_NO,CHASSIS_NO,GRN_DATE,COM_REF_NO,GRNA_NO,ITEM_CBM,WMS_COM_CODE,WMS_WH_CODE,WMS_BIN_CODE,MFC_CODE) VALUES " + " ('" + STR_YEAR_SEQ_NO + "','" + STR_TRANSACTION_LOCATION + "','" + STR_DOC_NO + "',TO_DATE('" + STR_DOC_DATE + "','dd/MM/yyyy'),'" + STR_BIN_LOCA + "'," + STR_ITEM_LINE_NO + " ,'" + STR_ITEM_CODE + "','" + STR_STATUS + "',0,'" + STR_SERIAL_NO + "','" + STR_WARRENTY_NO + "'," + STR_UNIT_COST + "," + STR_QTY + "," + STR_UPDATE_LINE_NO + ",'" + STR_DOC_REF_NO + "'," + M_Serial_Line + ",'" + STR_CHASSIS_NO + "',TO_DATE('" + STR_COM_GRN_DATE + "','DD/MM/YYYY'),'" + STR_COM_GRN_NO + "','" + STR_GRAN_NO + "'," + STR_ITEM_CBM + ",'" + STR_WMS_COM_CODE + "','" + STR_WMS_WH_CODE + "' ,'" + STR_WMS_BIN_CODE + "','" + STR_MFC_CODE + "')";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _ref = _oCom.ExecuteNonQuery();
            M_Serial_Line += 1;
        }

        #endregion save mov item serials

        #region Allow_Decimal

        public bool Allow_Decimal(string _ItemCode)
        {
            bool functionReturnValue = false;
            OracleDataReader _OrdSub = default(OracleDataReader);
            _sql = "SELECT SCM_ITEM_MASTER.BASE_UOM, SCM_ITEM_MASTER.ITEM_CODE FROM SCM_UOM_MASTER INNER JOIN SCM_ITEM_MASTER ON SCM_UOM_MASTER.UOM_CODE = SCM_ITEM_MASTER.BASE_UOM WHERE (SCM_ITEM_MASTER.ITEM_CODE =:itemcode) AND SCM_UOM_MASTER.ALLOW_DECIMAL =1";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
            _OrdSub = _oCom.ExecuteReader();
            if (_OrdSub.HasRows == true)
            {
                functionReturnValue = true;
            }
            else
            {
                functionReturnValue = false;
            }
            _OrdSub.Close();
            return functionReturnValue;
        }

        #endregion Allow_Decimal

        #region Insert Location Inventory

        private void Insert_Location_Inventory(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty_In_Hand, double Str_Free_Qty, double Str_Res_Qty, string Str_User)
        {
            _ref = 0;

            _sql = "UPDATE INV_LOCATION_INVENTORY_HEADER SET QTY_IN_HAND = QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_fqty, RESERVED_QTY = RESERVED_QTY + :p_rqty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_DATE = CURRENT_DATE " + " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        #endregion Insert Location Inventory

        #region Update Location Inventory - Minus Entries

        private void Update_Location_Inventory(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty_In_Hand, double Str_Free_Qty, double Str_Res_Qty, string Str_User)
        {
            _sql = "UPDATE INV_LOCATION_INVENTORY_HEADER SET QTY_IN_HAND = QTY_IN_HAND - :p_qty, FREE_QTY = FREE_QTY - :p_fqty, ISSUED_QTY = ISSUED_QTY + :p_qty , LAST_MODIFY_BY = :p_user, LAST_MODIFY_DATE = CURRENT_DATE " +
            " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        private void Update_FIFO_New(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty, string Str_In_Doc_No, Int16 Str_In_Itm_Line, Int16 Str_In_Batch_Line, string Str_User)
        {
            //GRN_LINE_NO     --> ITEM LINE
            //TOBOND_LINE_NO  --> BATCH LINE
            _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY =:loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
             " WHERE COMPANY_CODE =:comcode AND LOCATION_CODE =:loccode AND DOC_REF_NO =:docrefno AND GRN_LINE_NO =:itmline AND TOBOND_LINE_NO =:batchline AND ITEM_CODE_GRN =:itemcode AND STATUS =:itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        private void Remove_Item_Serial_Details(string Str_Company_Code, string Str_Location_Code, string Str_Doc_No, string Str_Bin_Location, string Str_Item_Code, string Str_Item_Status, string Str_Serial_No, string Str_Ser_ID)
        {
            _ref = 0;
            _sql = "UPDATE INV_ITEM_SERIAL_DETAILS SET AVAILABILITY ='NO' " +
                " WHERE COMPANY_CODE =:p_comcode AND LOCATION_CODE =:p_loccode AND DOC_REF_NO =:p_docno AND ITEM_CODE = :p_itemcode AND ITEM_STATUS =:p_itemstatus AND SERIAL_NO =:p_serno AND GRNA_NO =:p_grna ";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_comcode", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_loccode", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_serno", OracleDbType.NVarchar2).Value = Str_Serial_No;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_Ser_ID;
            _ref = _oCom.ExecuteNonQuery();
        }

        #endregion Update Location Inventory - Minus Entries

        #region Update Warranty Master

        //Add Chamal 27-Oct-2012
        private void Update_SCMWarrantyMaster(string _docNo, DateTime _docDate, string _custCode, string _custName, string _custAdd, string _custTel, string _invcNo, string _srCode, string _srName, string _vehiNo)
        {
            _ref = 0;
            _sql = "UPDATE temp_wara_upload " +
            "SET transactionno =:dono, warrantystartdate =:dodate, customercode =:custcode, customername =:custname, customeraddressinvoce =:custadd, " +
            "customerphoneno =:custtel, invoiceno =:invcno, showroom =:srcode, showroomname =:srname, vhlregno =:vehino, " +
            "updatedby ='EMS', updateddate =current_date " +
            "WHERE warrantyno =:warrno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":dono", OracleDbType.NVarchar2).Value = _docNo;
            _oCom.Parameters.Add(":dodate", OracleDbType.Date).Value = _docDate.Date;
            _oCom.Parameters.Add(":custcode", OracleDbType.NVarchar2).Value = _custCode;
            _oCom.Parameters.Add(":custname", OracleDbType.NVarchar2).Value = _custName;
            _oCom.Parameters.Add(":custadd", OracleDbType.NVarchar2).Value = _custAdd;
            _oCom.Parameters.Add(":custtel", OracleDbType.NVarchar2).Value = _custTel;
            _oCom.Parameters.Add(":invcno", OracleDbType.NVarchar2).Value = _invcNo;
            _oCom.Parameters.Add(":srcode", OracleDbType.NVarchar2).Value = _srCode;
            _oCom.Parameters.Add(":srname", OracleDbType.NVarchar2).Value = _srName;
            _oCom.Parameters.Add(":vehino", OracleDbType.NVarchar2).Value = _vehiNo;
            _ref = _oCom.ExecuteNonQuery();
        }

        public string GetSCMWarrNo(string _ItemCode, string _SerNo)
        {
            string _warrNo = string.Empty;
            OracleDataReader Ord = default(OracleDataReader);
            _sql = "SELECT WARRANTYNO FROM TEMP_WARA_UPLOAD WHERE ITEMCODE =:itemcode AND SERIALNO =:serno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = _ItemCode;
            _oCom.Parameters.Add(":serno", OracleDbType.NVarchar2).Value = _SerNo;
            Ord = _oCom.ExecuteReader();
            if (Ord.HasRows == true)
            {
                while (Ord.Read())
                {
                    _warrNo = Ord["WARRANTYNO"].ToString();
                    break;
                }
            }
            else
            {
                _warrNo = string.Empty;
            }
            Ord.Close();
            return _warrNo;
        }

        #endregion Update Warranty Master

        #endregion Insert/Update Movement/Inventory Table

        #region Get Item Details

        public bool Get_Item_Code_Details(string Str_ItemCode)
        {
            bool functionReturnValue = false;
            OracleDataReader OrdModel = default(OracleDataReader);

            functionReturnValue = false;

            _sql = "SELECT DESCRIPTION, MODEL_NO, BRAND_CODE, CATOGARY_1_CODE, CATOGARY_2_CODE, CATOGARY_3_CODE, BASE_UOM, SERIALIZE, WARRANTY FROM SCM_ITEM_MASTER WHERE (ITEM_CODE = :p_itemcode)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
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
            _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
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

        #endregion Get Item Details

        #region Get Location Details

        public bool Get_Loc_Details(string Str_LocCode, string Str_ComCode)
        {
            bool functionReturnValue = false;
            OracleDataReader OrdModel = default(OracleDataReader);

            functionReturnValue = false;

            _sql = "SELECT * FROM MST_LOC WHERE MST_LOC.ML_LOC_CD =:p_loc_code AND MST_LOC.ML_COM_CD =:p_com_code";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_loc_code", OracleDbType.NVarchar2).Value = Str_LocCode;
            _oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_ComCode;
            OrdModel = _oCom.ExecuteReader();
            if (OrdModel.HasRows == true)
            {
                while (OrdModel.Read())
                {
                    functionReturnValue = true;
                    loc_Ope = OrdModel["ML_OPE_CD"].ToString();
                    loc_Chnl = OrdModel["ML_CATE_2"].ToString();
                }
            }
            OrdModel.Close();
            return functionReturnValue;
        }

        #endregion Get Location Details

        #region Get Auto Number

        private void Update_Auto_Number(string Str_Module_ID, string Str_StartChar, string Str_Location_Code = "", bool Str_Loc_Status = false)
        {
            Int32 n_Number = 0;

            if (Str_Loc_Status == false)
            {
                _sql = "SELECT * FROM SCM_AUTO_NUMBER WHERE MODULE_ID = :p_moduleid AND START_CHAR = :p_startchar";
                OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        n_Number = (Int32)_oRd["NUMBER_N"] + 1;
                    }
                    _sql = "UPDATE SCM_AUTO_NUMBER SET NUMBER_N = :p_num WHERE MODULE_ID = :p_moduleid AND START_CHAR = :p_startchar";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Parameters.Add(":p_num", OracleDbType.Int32).Value = n_Number;
                    _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                    _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                    _oCom.CommandType = CommandType.Text;
                    _oCom.ExecuteNonQuery();
                }
                else
                {
                    _sql = "INSERT INTO SCM_AUTO_NUMBER (MODULE_ID,START_CHAR,NUMBER_N) VALUES (:p_moduleid, :p_startchar, 2)";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                    _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                    _oCom.CommandType = CommandType.Text;
                    _oCom.ExecuteNonQuery();
                }
                _oRd.Close();
            }
            else
            {
                _sql = "SELECT * FROM SCM_AUTO_NUMBER WHERE MODULE_ID= :p_moduleid AND START_CHAR = :p_startchar AND LOCATION_CODE= :p_loca";
                OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Location_Code;
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        string _num = Convert.ToString(_oRd["NUMBER_N"]);

                        n_Number = Convert.ToInt32(_num) + 1;
                    }

                    _sql = "UPDATE SCM_AUTO_NUMBER SET NUMBER_N = :p_num WHERE MODULE_ID = :p_moduleid AND START_CHAR = :p_startchar AND LOCATION_CODE = :p_loca";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Parameters.Add(":p_num", OracleDbType.Int32).Value = n_Number;
                    _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                    _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                    _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Location_Code;
                    _oCom.CommandType = CommandType.Text;
                    _oCom.ExecuteNonQuery();
                }
                else
                {
                    _sql = "INSERT INTO SCM_AUTO_NUMBER (MODULE_ID,START_CHAR,NUMBER_N,LOCATION_CODE) VALUES (:p_moduleid, :p_startchar, 2, :p_loca)";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                    _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                    _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Location_Code;
                    _oCom.CommandType = CommandType.Text;
                    _oCom.ExecuteNonQuery();
                }
                _oRd.Close();
            }
        }

        public string Get_System_Auto_Number(string Str_Module_ID, string Str_StartChar, string Str_Format_Number, string Str_Location_Code = "", bool Str_Loc_Status = false)
        {
            string Str_No = "";

            if (Str_Loc_Status == false)
            {
                _sql = "SELECT * FROM SCM_AUTO_NUMBER WHERE MODULE_ID = :p_moduleid AND START_CHAR = :p_startchar";

                OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        Str_No = _oRd["START_CHAR"].ToString() + String.Format(_oRd["NUMBER_N"].ToString(), Str_Format_Number);
                    }
                }
                else
                {
                    Str_No = Str_StartChar + String.Format("1", Str_Format_Number);
                }
                _oRd.Close();
            }
            else
            {
                _sql = "SELECT * FROM SCM_AUTO_NUMBER WHERE MODULE_ID= :p_moduleid AND START_CHAR = :p_startchar AND LOCATION_CODE= :p_loca";
                OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_moduleid", OracleDbType.NVarchar2).Value = Str_Module_ID;
                _oCom.Parameters.Add(":p_startchar", OracleDbType.NVarchar2).Value = Str_StartChar;
                _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Location_Code;
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        Str_No = _oRd["LOCATION_CODE"].ToString() + "-" + _oRd["START_CHAR"].ToString() + Convert.ToInt32(_oRd["NUMBER_N"]).ToString(Str_Format_Number, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    Str_No = Str_Location_Code + "-" + Str_StartChar + 1.ToString(Str_Format_Number, CultureInfo.InvariantCulture);
                }
                _oRd.Close();
            }

            return Str_No;
        }

        #endregion Get Auto Number

        #region Get Default Bin

        public string Get_Default_Bin(string Str_LocCode, string Str_ComCode)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ord = default(OracleDataReader);

            _sql = "SELECT inr_bin_loc.ibl_bin_cd FROM inr_bin_loc " +
          "WHERE inr_bin_loc.ibl_com_cd = :p_com_code AND inr_bin_loc.ibl_loc_cd = :p_loc_code " +
          "AND inr_bin_loc.ibl_act =1 AND inr_bin_loc.ibl_is_def=1";

            _sql = "SELECT BN_BIN_CODE FROM WH_R_BIN_LOCATION_DEFINITION WHERE BN_LOCATION =:p_loc_code AND BN_DEFAULT =1";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":p_loc_code", OracleDbType.NVarchar2).Value = Str_LocCode;
            //_oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_ComCode;
            Ord = _oCom.ExecuteReader();
            if (Ord.HasRows == true)
            {
                while (Ord.Read())
                {
                    functionReturnValue = Ord["BN_BIN_CODE"].ToString();
                    break;
                }
            }
            else
            {
                functionReturnValue = "NOT";
            }
            Ord.Close();
            return functionReturnValue;
        }

        #endregion Get Default Bin

        #region Get EMS Item Status

        public string Get_EMS_Item_Status(string Str_SCM_ItemStatus)
        {
            string functionReturnValue = string.Empty;
            OracleDataReader Ord = default(OracleDataReader);

            _sql = "SELECT MIS_CD FROM MST_ITM_STUS WHERE MIS_OLD_CD =:itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = Str_SCM_ItemStatus;
            //_oCom.Parameters.Add(":p_com_code", OracleDbType.NVarchar2).Value = Str_ComCode;
            Ord = _oCom.ExecuteReader();
            if (Ord.HasRows == true)
            {
                while (Ord.Read())
                {
                    functionReturnValue = Ord["MIS_CD"].ToString();
                    break;
                }
            }
            else
            {
                functionReturnValue = "NOT";
            }
            Ord.Close();
            return functionReturnValue;
        }

        #endregion Get EMS Item Status

        #region Get Data - EMS

        private DataTable GetALL_HPT_ACC_Table(string _acc_no)
        {
            _sql = "SELECT * FROM EMS.HPT_ACC WHERE HPA_ACC_NO =:hpa_acc_no";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":hpa_acc_no", OracleDbType.NVarchar2).Value = _acc_no;
            OracleDataAdapter adp = new OracleDataAdapter(_oCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "HPT_ACC");
            adp.Dispose();

            //if (ds.Tables["HPT_ACC"].Rows.Count > 0)
            //{
            //    ds.Tables["HPT_ACC"];
            //}

            return ds.Tables["HPT_ACC"];
        }

        private DataTable GetALL_SAT_RECEIPT_Table(string _receipt_no)
        {
            _sql = "SELECT * FROM EMS.SAT_RECEIPT WHERE SAR_RECEIPT_NO =:sar_receipt_no";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":sar_receipt_no", OracleDbType.NVarchar2).Value = _receipt_no;
            OracleDataAdapter adp = new OracleDataAdapter(_oCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "SAT_RECEIPT");
            adp.Dispose();
            return ds.Tables["SAT_RECEIPT"];
        }

        private DataTable GetALL_SAT_RECEIPTITM_Table(string _recNo)
        {
            _sql = "SELECT * FROM EMS.SAT_RECEIPTITM WHERE SARD_RECEIPT_NO =:sard_receipt_no";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":sard_receipt_no", OracleDbType.NVarchar2).Value = _recNo;
            OracleDataAdapter adp = new OracleDataAdapter(_oCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "SAT_RECEIPTITM");
            adp.Dispose();
            return ds.Tables["SAT_RECEIPTITM"];
        }

        private DataTable GetALL_HPT_RVT_DET_Table(string _seq)
        {
            _sql = "SELECT * FROM EMS.HPT_RVT_DET WHERE HRD_SEQ =:hrd_seq";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":hrd_seq", OracleDbType.NVarchar2).Value = _seq;
            OracleDataAdapter adp = new OracleDataAdapter(_oCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "HPT_RVT_DET");
            adp.Dispose();
            return ds.Tables["HPT_RVT_DET"];
        }

        private DataTable GetALL_HPT_ACC_LOG_Table(string _seq)
        {
            _sql = "SELECT * FROM EMS.HPT_ACC_LOG WHERE HRD_SEQ =:hrd_seq";
            OracleCommand _oCom = new OracleCommand(_sql, _emsSalesDAL.oConnection) { CommandType = CommandType.Text, BindByName = true };
            _oCom.Parameters.Add(":hrd_seq", OracleDbType.NVarchar2).Value = _seq;
            OracleDataAdapter adp = new OracleDataAdapter(_oCom);
            DataSet ds = new DataSet();
            adp.Fill(ds, "HPT_ACC_LOG");
            adp.Dispose();
            return ds.Tables["HPT_ACC_LOG"];
        }

        #endregion Get Data - EMS



        //Tharanga 
        public Int32 UpdateTempWaraUpload_Save(string p_transactionno, string Jrd_itm_cd, string Jrd_model, string Jrd_ser1,
            string Jrd_warr, DateTime Jrd_warrstartdt, Int32 Jrd_warrperiod, string Jrd_invc_no, string p_customerCD, string p_customerName,
            string p_customerADD, string p_customerphoneno, DateTime Jrd_date_pur, string Jrd_brand, string Jrd_itm_desc, string Jrd_loc,
             string p_account, string Jrd_chg_warr_rmk, string p_grnno, DateTime GRN_Date, string Jrd_supp_cd,string p_chassis, decimal Jrd_itm_cost,
            string Jrd_itm_stus, string Jrd_regno, string p_frm_loc)
        {
            OracleParameter[] param = new OracleParameter[30];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_transactionno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_transactionno;
            (param[1] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_itm_cd;
            (param[2] = new OracleParameter("p_modelno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =Jrd_model;
            (param[3] = new OracleParameter("p_serialno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_ser1;

            (param[4] = new OracleParameter("p_warrantyno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_warr;
            (param[5] = new OracleParameter("p_warrantystartdate", OracleDbType.Date, null, ParameterDirection.Input)).Value =Jrd_warrstartdt;
            (param[6] = new OracleParameter("p_warrantyperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Jrd_warrperiod;
            (param[7] = new OracleParameter("p_invoiceno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_invc_no;
            (param[8] = new OracleParameter("p_customercode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerCD;
            (param[9] = new OracleParameter("p_customername", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerName;
            (param[10] = new OracleParameter("p_customeraddressinvoce", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerADD;
            (param[11] = new OracleParameter("p_customerphoneno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_customerphoneno;
            (param[12] = new OracleParameter("p_podate", OracleDbType.Date, null, ParameterDirection.Input)).Value = Jrd_date_pur;
            (param[13] = new OracleParameter("p_itmbrand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_brand;
            (param[14] = new OracleParameter("p_itmdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =Jrd_itm_desc;
            (param[15] = new OracleParameter("p_transaction_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_loc;
            (param[16] = new OracleParameter("p_locdesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_loc;
            (param[17] = new OracleParameter("p_salesorderno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[18] = new OracleParameter("p_accno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_account;
            (param[19] = new OracleParameter("p_warrlineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;////
            (param[20] = new OracleParameter("p_warranty_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_chg_warr_rmk;
            (param[21] = new OracleParameter("p_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_grnno;
            (param[22] = new OracleParameter("p_grndate", OracleDbType.Date, null, ParameterDirection.Input)).Value = GRN_Date;
            (param[23] = new OracleParameter("p_supcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_supp_cd;
            (param[24] = new OracleParameter("p_chassis", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_chassis;
            (param[25] = new OracleParameter("p_unitprice", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Jrd_itm_cost;
            (param[26] = new OracleParameter("p_itemstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_itm_stus;
            (param[27] = new OracleParameter("p_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Jrd_regno;
            (param[28] = new OracleParameter("p_frm_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_frm_loc;
           

            param[29] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_upd_temp_wara_save", CommandType.StoredProcedure, param);
            return effects;
        }


           
         

    }
}