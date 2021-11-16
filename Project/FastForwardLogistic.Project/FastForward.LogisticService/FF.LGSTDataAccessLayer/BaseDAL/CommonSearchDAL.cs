using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.BusinessObjects.Genaral;
using Oracle.DataAccess.Client;
using System.Data;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;


namespace FF.DataAccessLayer.BaseDAL
{
    public class CommonSearchDAL : BaseDAL
    {
        /// <summary>
        /// Isuru 2017/05/26
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getcustomerdet(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Customer Code":
                        searchFld = "MBE_CD";
                        break;
                    case "Name":
                        searchFld = "MBE_NAME";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<cus_details> itmList = new List<cus_details>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUS_DETAILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<cus_details>(_dtResults, cus_details.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Isuru 2017/05/30
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerExecutive(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Executive Code":
                        searchFld = "ESEP_CD";
                        break;
                    case "Name":
                        searchFld = "ESEP_NAME_INITIALS";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<cus_details> itmList = new List<cus_details>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_EXEC_DET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<cus_details>(_dtResults, cus_details.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSTOMERDETAILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //added by dilshan on 26/01/2017
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "sp_search_customerbytype", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //added by dilshan on 26/01/2017
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByType_New(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "sp_search_customerbytype_new", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsByJobNo(string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type = null)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[6] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSTOMERDETAILSBYJOB", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsWithtype(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type,string _custype)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[6] = new OracleParameter("p_ctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custype;
                param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSTOMERWITHTYPE", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<JOB_NUM_SEARCH> getJobNumber(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "JB_JB_DT";
                        break;
                    case "Pouch No":
                        searchFld = "JB_POUCH_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<JOB_NUM_SEARCH> itmList = new List<JOB_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[7] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_LGT_JOB", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_dtResults, JOB_NUM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //dilshan
        public List<JOB_NUM_SEARCH> GetAllJobNumber(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "JB_JB_DT";
                        break;
                    case "Pouch No":
                        searchFld = "JB_POUCH_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<JOB_NUM_SEARCH> itmList = new List<JOB_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[7] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_ALL_JOB", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_dtResults, JOB_NUM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CustomerSearchObject> getCustomer(string telephone, string email)
        {

            try
            {
                List<CustomerSearchObject> _customer = new List<CustomerSearchObject>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_search_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = email;
                (param[1] = new OracleParameter("p_search_mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = telephone;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_SEARCH_CUSTOMER", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    _customer = DataTableExtensions.ToGenericList<CustomerSearchObject>(_dtResults, CustomerSearchObject.Converter);
                }
                return _customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-06-28
        public List<BL_NUM_SEARCH> getBLNumber(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "BL_DOC_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<BL_NUM_SEARCH> itmList = new List<BL_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_BLTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bltype;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_DOC_NO", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<BL_NUM_SEARCH>(_dtResults, BL_NUM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<BL_NUM_SEARCH> getBLNumberDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "BL_DOC_NO";
                        break;                    
                    default:
                        searchFld = "";
                        break;
                }
                List<BL_NUM_SEARCH> itmList = new List<BL_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_BLTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bltype;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_DOC_NO_DF", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<BL_NUM_SEARCH>(_dtResults, BL_NUM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<BL_NUM_SEARCH_MBL> getBLNumberDfMBL(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Master BL":
                        searchFld = "BL_DOC_NO";
                        break;
                    case "House BL":
                        searchFld = "BL_H_DOC_NO";
                        break;
                    case "Draft BL":
                        searchFld = "BL_D_DOC_NO";
                        break;
                    case "Date":
                        searchFld = "BL_DOC_DT";
                        break;
                    case "Job No":
                        searchFld = "BL_JOB_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<BL_NUM_SEARCH_MBL> itmList = new List<BL_NUM_SEARCH_MBL>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_BLTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bltype;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_DOC_NO_DFMBL", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<BL_NUM_SEARCH_MBL>(_dtResults, BL_NUM_SEARCH_MBL.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //subodana 2017-06-28
        public List<UOM_SEARCH> getUOM(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<UOM_SEARCH> itmList = new List<UOM_SEARCH>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_UOM", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<UOM_SEARCH>(_dtResults, UOM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //subodana 2017-06-28
        public List<FIELD_SEARCH> getAllSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH> itmList = new List<FIELD_SEARCH>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("P_PGENUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("P_PAGESIZE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("P_FIELD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("P_SEARCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("P_COLM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
                param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_FIELDS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    if (column == "BL_MANUAL_H_REF1" || column == "BL_MANUAL_M_REF1")
                    {
                        itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH>(_dtResults, FIELD_SEARCH.ConverterBL);
                    }
                    else
                    {
                        itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH>(_dtResults, FIELD_SEARCH.Converter);
                    }
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FIELD_SEARCH_DF> getAllSearchDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Customer":
                        searchFld = "BL_CUS_CD";
                        break;
                    case "Agent":
                        searchFld = "BL_AGENT_CD";
                        break;
                    case "Date":
                        searchFld = "BL_DOC_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH_DF> itmList = new List<FIELD_SEARCH_DF>();
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("P_PGENUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("P_PAGESIZE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("P_FIELD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("P_SEARCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("P_COLM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[7] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_FIELDSDF", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH_DF>(_dtResults, FIELD_SEARCH_DF.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBL(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Customer":
                        searchFld = "BL_CUS_CD";
                        break;
                    case "Agent":
                        searchFld = "BL_AGENT_CD";
                        break;
                    case "Date":
                        searchFld = "BL_DOC_DT";
                        break;
                    case "JobNo":
                        searchFld = "BL_JOB_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH_DF_HBL> itmList = new List<FIELD_SEARCH_DF_HBL>();
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("P_PGENUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("P_PAGESIZE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("P_FIELD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("P_SEARCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("P_COLM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[7] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_FIELDSDFHBL", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH_DF_HBL>(_dtResults, FIELD_SEARCH_DF_HBL.ConverterbyHBL);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FIELD_SEARCH_DF_HBL> getAllSearchDfHBLJob(DateTime _fromDate, DateTime _toDate, string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Customer":
                        searchFld = "BL_CUS_CD";
                        break;
                    case "Agent":
                        searchFld = "BL_AGENT_CD";
                        break;
                    case "Date":
                        searchFld = "BL_DOC_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH_DF_HBL> itmList = new List<FIELD_SEARCH_DF_HBL>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("P_PGENUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("P_PAGESIZE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("P_FIELD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("P_SEARCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("P_COLM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
                (param[5] = new OracleParameter("P_FRMDTE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("P_TODTE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[7] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_FIELDSDFHBLJOB", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    if (column == "BL_H_DOC_NO")
                    {
                        itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH_DF_HBL>(_dtResults, FIELD_SEARCH_DF_HBL.ConverterbyHBL);
                    }
                    else
                    {
                        itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH_DF_HBL>(_dtResults, FIELD_SEARCH_DF_HBL.Converter);
                    } 
                    //itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH_DF_HBL>(_dtResults, FIELD_SEARCH_DF_HBL.ConverterbyHBL);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FIELD_SEARCH> getAllSearchByJobNo(string jobno, string pgeNum, string pgeSize, string searchFld, string searchVal, string column)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH> itmList = new List<FIELD_SEARCH>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("P_PGENUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("P_PAGESIZE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("P_FIELD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("P_SEARCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("P_COLM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
                (param[5] = new OracleParameter("P_JOBNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "sp_search_bl_fields_byjob", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH>(_dtResults, FIELD_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Pay_type> GetPayTypes()
        {
            try
            {
                List<Pay_type> _paytypes = new List<Pay_type>();
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PAY_TYPE", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    _paytypes = DataTableExtensions.ToGenericList<Pay_type>(_dtResults, Pay_type.Converter);
                }
                return _paytypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-06-29
        public List<SEARCH_PORT> getPorts(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_PORT> itmList = new List<SEARCH_PORT>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_PORT", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_PORT>(_dtResults, SEARCH_PORT.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Dilshan 2017-09-11
        public List<SEARCH_PORT> getPortsRef(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_PORT> itmList = new List<SEARCH_PORT>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "sp_search_portref", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_PORT>(_dtResults, SEARCH_PORT.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //subodana 2017-06-29
        public List<SEARCH_VESSEL> getVessels(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_VESSEL> itmList = new List<SEARCH_VESSEL>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_VESSEL", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_VESSEL>(_dtResults, SEARCH_VESSEL.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Dilshan 2017-09-11
        public List<SEARCH_VESSEL> getVesselsRef(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_VESSEL> itmList = new List<SEARCH_VESSEL>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "sp_search_vesselref", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_VESSEL>(_dtResults, SEARCH_VESSEL.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Pay_type> GetInwordTypes()
        {
            try
            {
                List<Pay_type> _paytypes = new List<Pay_type>();
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INWORDTYPE", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    _paytypes = DataTableExtensions.ToGenericList<Pay_type>(_dtResults, Pay_type.Converter);
                }
                return _paytypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-06-30
        public List<SEARCH_MESURE_TP> Get_Mesure_Tp(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Description":
                        searchFld = "DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_MESURE_TP> itmList = new List<SEARCH_MESURE_TP>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_MESURE_TP", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_MESURE_TP>(_dtResults, SEARCH_MESURE_TP.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Added by Chathura on 15-sep-2017 and commented below
        public List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId, string userDefChnl)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "mpc_cd";
                    break;
                case "Description":
                    searchFld = "mpc_desc";
                    break;
                case "Address":
                    searchFld = "mpc_add1";
                    break;
                case "Chanel":
                    searchFld = "mpc_chnl";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_PC_SEARCH> profCenters = new List<MST_PC_SEARCH>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            (param[6] = new OracleParameter("p_chanel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefChnl;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_USERPROCENTER", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                profCenters = DataTableExtensions.ToGenericList<MST_PC_SEARCH>(_dtResults, MST_PC_SEARCH.Converter);
            }
            return profCenters;
        }

        //public List<MST_PC_SEARCH> getUserProfitCenters(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        //{
        //    string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
        //    switch (searchFld)
        //    {
        //        case "Code":
        //            searchFld = "mpc_cd";
        //            break;
        //        case "Description":
        //            searchFld = "mpc_desc";
        //            break;
        //        case "Address":
        //            searchFld = "mpc_add1";
        //            break;
        //        case "Chanel":
        //            searchFld = "mpc_chnl";
        //            break;
        //        default:
        //            searchFld = "";
        //            break;

        //    }
        //    List<MST_PC_SEARCH> profCenters = new List<MST_PC_SEARCH>();
        //    OracleParameter[] param = new OracleParameter[7];
        //    (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
        //    (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
        //    (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
        //    (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
        //    (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
        //    (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
        //    param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
        //    DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_USERPROCEN", CommandType.StoredProcedure, false, param);

        //    if (_dtResults.Rows.Count > 0)
        //    {
        //        profCenters = DataTableExtensions.ToGenericList<MST_PC_SEARCH>(_dtResults, MST_PC_SEARCH.Converter);
        //    }
        //    return profCenters;
        //}

        // Added by Chathura 13-Sep-2017
        public List<MST_MS_SEARCH> getModeOfShipment(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "mms_cd";
                    break;
                case "Description":
                    searchFld = "mms_desc";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_MS_SEARCH> profCenters = new List<MST_MS_SEARCH>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_MOFSHIPMENT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                profCenters = DataTableExtensions.ToGenericList<MST_MS_SEARCH>(_dtResults, MST_MS_SEARCH.Converter);
            }
            return profCenters;
        }

        public List<MST_COM_SEARCH> getUserCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string userId)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "mc_cd";
                    break;
                case "Description":
                    searchFld = "mc_desc";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COM_SEARCH> profCenters = new List<MST_COM_SEARCH>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "sp_search_usercompany", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                profCenters = DataTableExtensions.ToGenericList<MST_COM_SEARCH>(_dtResults, MST_COM_SEARCH.Converter);
            }
            return profCenters;
        }
        public List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Epf No":
                    searchFld = "ESEP_EPF";
                    break;
                case "Category":
                    searchFld = "ESEP_CAT_SUBCD";
                    break;
                case "First Name":
                    searchFld = "ESEP_FIRST_NAME";
                    break;
                case "Last Name":
                    searchFld = "ESEP_LAST_NAME";
                    break;
                case "NIC":
                    searchFld = "ESEP_NIC";
                    break;
                case "MOBILE":
                    searchFld = "ESEP_MOBI_NO";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_EMPLOYEE_SEARCH_HEAD> emp = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_EMPLOYEEDETAILS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<MST_EMPLOYEE_SEARCH_HEAD>(_dtResults, MST_EMPLOYEE_SEARCH_HEAD.Converter);
            }
            return emp;
        }

        public List<MST_EMPLOYEE_SEARCH_HEAD> getEmployeeDetailsEx(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Epf No":
                    searchFld = "ESEP_EPF";
                    break;
                case "Category":
                    searchFld = "ESEP_CAT_SUBCD";
                    break;
                case "First Name":
                    searchFld = "ESEP_FIRST_NAME";
                    break;
                case "Last Name":
                    searchFld = "ESEP_LAST_NAME";
                    break;
                case "NIC":
                    searchFld = "ESEP_NIC";
                    break;
                case "MOBILE":
                    searchFld = "ESEP_MOBI_NO";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_EMPLOYEE_SEARCH_HEAD> emp = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_EMPLOYEEDETAILSEX", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<MST_EMPLOYEE_SEARCH_HEAD>(_dtResults, MST_EMPLOYEE_SEARCH_HEAD.Converter);
            }
            return emp;
        }

        public List<PETTYCASH_REQHDR_SRCHHED> ptyCshReqSearch(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Request No":
                    searchFld = "TPRH_REQ_NO";
                    break;
                case "Mannual Ref":
                    searchFld = "TPRH_MANUAL_REF";
                    break;
                case "Request Date":
                    searchFld = "TPRH_REQ_DT";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<PETTYCASH_REQHDR_SRCHHED> req = new List<PETTYCASH_REQHDR_SRCHHED>();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[6] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[7] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
            (param[8] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_PETTYCSHREQ", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<PETTYCASH_REQHDR_SRCHHED>(_dtResults, PETTYCASH_REQHDR_SRCHHED.Converter);
            }
            return req;
        }

        public List<CONS_SEARCH_HEAD> getConsigneeDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Consignee Code":
                    searchFld = "MBE_CD";
                    break;
                case "Account Code":
                    searchFld = "MBE_ACC_CD";
                    break;
                case "Name":
                    searchFld = "MBE_NAME";
                    break;
                case "Add1":
                    searchFld = "MBE_ADD1";
                    break;
                case "Mobile":
                    searchFld = "MBE_MOB";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<CONS_SEARCH_HEAD> req = new List<CONS_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_CONSIGNEECUS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<CONS_SEARCH_HEAD>(_dtResults, CONS_SEARCH_HEAD.Converter);
            }
            return req;
        }

        public List<JOB_NUM_SEARCH> getPettyCashJobSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Job No":
                    searchFld = "JB_JB_NO";
                    break;
                case "Pouch No":
                    searchFld = "JB_POUCH_NO";
                    break;
                case "Job Date":
                    searchFld = "JB_JB_DT";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<JOB_NUM_SEARCH> req = new List<JOB_NUM_SEARCH>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_PTYJOB", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_dtResults, JOB_NUM_SEARCH.ConverterJob);
            }
            return req;
        }

        public List<MST_COST_ELEMENT_SEARCH> getCostElemts(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Cost Code":
                    searchFld = "MCE_CD";
                    break;
                case "Description":
                    searchFld = "MCE_DESC";
                    break;
                case "Account Code":
                    searchFld = "MCE_ACC_CD";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COST_ELEMENT_SEARCH> req = new List<MST_COST_ELEMENT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_COSTELEMENT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<MST_COST_ELEMENT_SEARCH>(_dtResults, MST_COST_ELEMENT_SEARCH.Converter);
            }
            return req;
        }

        public List<MST_COST_ELEMENT_SEARCH> getCostElemtsRef(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Cost Code":
                    searchFld = "MCE_CD";
                    break;
                case "Description":
                    searchFld = "MCE_DESC";
                    break;
                case "Account Code":
                    searchFld = "MCE_ACC_CD";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COST_ELEMENT_SEARCH> req = new List<MST_COST_ELEMENT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "sp_search_costelementref", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<MST_COST_ELEMENT_SEARCH>(_dtResults, MST_COST_ELEMENT_SEARCH.Converter);
            }
            return req;
        }

        public List<MST_CUR_SEARCH> getCurrency(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MCR_CD";
                    break;
                case "Description":
                    searchFld = "MCR_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_CUR_SEARCH> req = new List<MST_CUR_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_CURRENCY", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<MST_CUR_SEARCH>(_dtResults, MST_CUR_SEARCH.Converter);
            }
            return req;
        }

        public List<FTW_VEHICLE_NO_SEARCH> getTelVehLcDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "FVN_CD";
                    break;
                case "Description":
                    searchFld = "FVN_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<FTW_VEHICLE_NO_SEARCH> req = new List<FTW_VEHICLE_NO_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_TELVEHLC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<FTW_VEHICLE_NO_SEARCH>(_dtResults, FTW_VEHICLE_NO_SEARCH.Converter);
            }
            return req;
        }
        //subodana 2017-07-06
        public List<SEARCH_INVOICE> getInvoiceNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NO", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SEARCH_INVOICE> getInvoiceNoCrd(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Credit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NOCRD", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //subodana 2017-07-31
        public List<SEARCH_INVOICE> getDebitNoteNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_DEBT_NO", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SEARCH_INVOICE_BAL> getInvoiceNoByCus(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string Customer, string pc, string othpc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE_BAL> itmList = new List<SEARCH_INVOICE_BAL>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Customer;
                (param[6] = new OracleParameter("P_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[7] = new OracleParameter("P_othpc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = othpc;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NO_BY_CUS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE_BAL>(_dtResults, SEARCH_INVOICE_BAL.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<PETTYCASH_SETTLE_SEARCH> getSettlementList(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Settlement No":
                        searchFld = "TPSH_SETTLE_NO";
                        break;
                    case "Mannual Ref":
                        searchFld = "TPSH_MAN_REF";
                        break;
                    case "Settlement Date":
                        searchFld = "TPSH_SETTLE_DT";
                        break;
                    case "Job No":
                        searchFld = "TPSD_JOB_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<PETTYCASH_SETTLE_SEARCH> itmList = new List<PETTYCASH_SETTLE_SEARCH>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_PETTYCSHREQSETT", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<PETTYCASH_SETTLE_SEARCH>(_dtResults, PETTYCASH_SETTLE_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MST_COUNTRY_SEARCH> getCountry(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MCU_CD";
                    break;
                case "Description":
                    searchFld = "MCU_DESC";
                    break;
                case "Region Code":
                    searchFld = "MCU_REGION_CD";
                    break;
                case "Capital":
                    searchFld = "MCU_CAPITAL";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COUNTRY_SEARCH> crg = new List<MST_COUNTRY_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_COUNTRY_EQ", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_COUNTRY_SEARCH>(_dtResults, MST_COUNTRY_SEARCH.Converter);
            }
            return crg;
        }
        public List<MST_TOWN_SEARCH_HEAD> getTownDetails(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Town":
                        searchFld = "mt_desc";
                        break;
                    case "District":
                        searchFld = "mdis_desc";
                        break;
                    case "Province":
                        searchFld = "mpro_desc";
                        break;
                    case "Code":
                        searchFld = "mt_cd";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_TOWN_SEARCH_HEAD> townlist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbltOWN", "SP_SEARCH_PERTOWNDETAILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    townlist = DataTableExtensions.ToGenericList<MST_TOWN_SEARCH_HEAD>(_dtResults, MST_TOWN_SEARCH_HEAD.Converter);
                }
                return townlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Dilshan 2017/09/13
        public List<MST_TOWN_SEARCH_HEAD> getTownwithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Town":
                        searchFld = "mt_desc";
                        break;
                    case "District":
                        searchFld = "mdis_desc";
                        break;
                    case "Province":
                        searchFld = "mpro_desc";
                        break;
                    case "Code":
                        searchFld = "mt_cd";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_TOWN_SEARCH_HEAD> townlist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_search1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal1;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbltOWN", "sp_search_pertownwithcountry", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    townlist = DataTableExtensions.ToGenericList<MST_TOWN_SEARCH_HEAD>(_dtResults, MST_TOWN_SEARCH_HEAD.Converter);
                }
                return townlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //dilshan on 12/05/2018
        public List<MST_DISTRICT_SEARCH> getDistrictwithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    //case "Town":
                    //    searchFld = "mt_desc";
                    //    break;
                    case "District":
                        searchFld = "mdis_desc";
                        break;
                    case "Province":
                        searchFld = "mpro_desc";
                        break;
                    case "Code":
                        searchFld = "mdis_cd";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_DISTRICT_SEARCH> townlist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_search1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal1;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbltOWN", "sp_search_perdiswithcountry", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    townlist = DataTableExtensions.ToGenericList<MST_DISTRICT_SEARCH>(_dtResults, MST_DISTRICT_SEARCH.Converter);
                }
                return townlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MST_PROVINCE_SEARCH> getProvincewithCountry(string pgeNum, string pgeSize, string searchFld, string searchVal, string searchVal1)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    //case "Town":
                    //    searchFld = "mt_desc";
                    //    break;
                    //case "District":
                    //    searchFld = "mdis_desc";
                    //    break;
                    case "Province":
                        searchFld = "mpro_desc";
                        break;
                    case "Code":
                        searchFld = "mpro_cd";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_PROVINCE_SEARCH> townlist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_search1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal1;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbltOWN", "sp_search_perprovwithcountry", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    townlist = DataTableExtensions.ToGenericList<MST_PROVINCE_SEARCH>(_dtResults, MST_PROVINCE_SEARCH.Converter);
                }
                return townlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-06-28
        public List<FIELD_SEARCH> getJobPouchSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Job No":
                        searchFld = "JOBNO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH> itmList = new List<FIELD_SEARCH>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_LGT_JOB_POUCH", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH>(_dtResults, FIELD_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FIELD_SEARCH2> getJobPouchSearch2(string pgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "CODE";
                        break;
                    case "Job No":
                        searchFld = "JOBNO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<FIELD_SEARCH2> itmList = new List<FIELD_SEARCH2>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_LGT_JOB_POUCH2", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<FIELD_SEARCH2>(_dtResults, FIELD_SEARCH2.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<MST_BANKACC_SEARCH_HEAD> getDepositedBanks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Account Code":
                    searchFld = "MSBA_ACC_CD";
                    break;
                case "Description":
                    searchFld = "MSBA_ACC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_BANKACC_SEARCH_HEAD> emp = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_BANKACCOUNTS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<MST_BANKACC_SEARCH_HEAD>(_dtResults, MST_BANKACC_SEARCH_HEAD.Converter);
            }
            return emp;
        }
        public List<MST_BUSCOM_BANK_SEARCH_HEAD> getBusComBanks(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MBI_ID";
                    break;
                case "Description":
                    searchFld = "MBI_DESC";
                    break;
                case "Id":
                    searchFld = "MBI_CD";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_BUSCOM_BANK_SEARCH_HEAD> busCom = new List<MST_BUSCOM_BANK_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_BUSCOMBANKACCOUNTS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                busCom = DataTableExtensions.ToGenericList<MST_BUSCOM_BANK_SEARCH_HEAD>(_dtResults, MST_BUSCOM_BANK_SEARCH_HEAD.Converter);
            }
            return busCom;
        }
        public List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> getBoscomBankBranchs(string bankcd, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MBB_CD";
                    break;
                case "Description":
                    searchFld = "MBB_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD> busComBranch = new List<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bankcd;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_BUSCOMBANKBRANCHES", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                busComBranch = DataTableExtensions.ToGenericList<MST_BUSCOM_BANKBRANCH_SEARCH_HEAD>(_dtResults, MST_BUSCOM_BANKBRANCH_SEARCH_HEAD.Converter);
            }
            return busComBranch;
        }
        public List<MST_RECEIPT_TYPE_SEARCH_HEAD> getReceiptTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Type":
                    searchFld = "MSRT_CD";
                    break;
                case "Description":
                    searchFld = "MSRT_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_RECEIPT_TYPE_SEARCH_HEAD> crg = new List<MST_RECEIPT_TYPE_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_RECEIPTTYPE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_RECEIPT_TYPE_SEARCH_HEAD>(_dtResults, MST_RECEIPT_TYPE_SEARCH_HEAD.Converter);
            }
            return crg;
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getUnallowReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string customer,string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Doc No":
                    searchFld = "SIR_RECEIPT_NO";
                    break;
                case "Ref No":
                    searchFld = "SIR_MANUAL_REF_NO";
                    break;
                case "Oth Ref No":
                    searchFld = "SIR_ANAL_3";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_RECEIPT_SEARCH_HEAD> crg = new List<MST_RECEIPT_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_profcen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profCen;
            (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
            (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
            (param[8] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;

            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_UNALRECEIPTDETAILS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_RECEIPT_SEARCH_HEAD>(_dtResults, MST_RECEIPT_SEARCH_HEAD.Converter);
            }
            return crg;
        }
        public List<MST_RECEIPT_SEARCH_HEAD> getReceiptEntries(string company, string profCen, DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Doc No":
                    searchFld = "SAR_RECEIPT_NO";
                    break;
                case "Ref No":
                    searchFld = "SAR_MANUAL_REF_NO";
                    break;
                case "Oth Ref No":
                    searchFld = "SAR_ANAL_3";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_RECEIPT_SEARCH_HEAD> crg = new List<MST_RECEIPT_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_profcen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profCen;
            (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
            (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_RECEIPTDETAILS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_RECEIPT_SEARCH_HEAD>(_dtResults, MST_RECEIPT_SEARCH_HEAD.Converter);
            }
            return crg;
        }
        public List<TYPE_OF_SHIPMENT> getShipmentTypes(string company, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "CODE";
                    break;
                case "Description":
                    searchFld = "DESCRIPTION";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<TYPE_OF_SHIPMENT> shpTypes = new List<TYPE_OF_SHIPMENT>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblSipmentTypes", "SP_SEARCH_SHIPMENTTYPE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                shpTypes = DataTableExtensions.ToGenericList<TYPE_OF_SHIPMENT>(_dtResults, TYPE_OF_SHIPMENT.Converter);
            }
            return shpTypes;
        }
        public List<Job_No_Search> getJobNoForPouch(string pgeNum, string pgeSize, string searchFld, string searchVal,string com, string pouch)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Job No":
                    searchFld = "JB_JB_NO";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<Job_No_Search> emp = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[5] = new OracleParameter("p_pouch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pouch;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_JONO_POUCH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<Job_No_Search>(_dtResults, Job_No_Search.Converter);
            }
            return emp;
        }

        // Added by Chathura on 20-sep-2017
        public List<MST_DIVISION_SEARCH> getDivisions(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "msrd_cd";
                    break;
                case "Description":
                    searchFld = "msrd_desc";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_DIVISION_SEARCH> divisions = new List<MST_DIVISION_SEARCH>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[6] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblProf", "SP_GET_MSRDIVITIONS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                divisions = DataTableExtensions.ToGenericList<MST_DIVISION_SEARCH>(_dtResults, MST_DIVISION_SEARCH.Converter);
            }
            return divisions;
        }

        public List<BL_NUM_SEARCH_DBL> getBLNumberDDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string bltype,string PC)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "BL_DOC_NO";
                        break;
                    case "Ref.No":
                        searchFld = "BL_MANUAL_D_REF";
                        break;
                    case "Pouch No":
                        searchFld = "BL_POUCH_NO";
                        break;
                    case "Date":
                        searchFld = "DATE_";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<BL_NUM_SEARCH_DBL> itmList = new List<BL_NUM_SEARCH_DBL>();
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_BLTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bltype;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[8] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_BL_DOC_NO_DF", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<BL_NUM_SEARCH_DBL>(_dtResults, BL_NUM_SEARCH_DBL.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_CUS_SEARCH_HEAD> getCustomerDetailsJobFiltered(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type, string _custype, string jobno)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Code":
                        searchFld = "Mbe_cd";
                        break;
                    case "Name":
                        searchFld = "Mbe_name";
                        break;
                    case "NIC":
                        searchFld = "Mbe_nic";
                        break;
                    case "Mobile":
                        searchFld = "Mbe_mob";
                        break;
                    case "BR No":
                        searchFld = "Mbe_br_no";
                        break;
                    case "Address1":
                        searchFld = "MBE_ADD1";
                        break;
                    case "Address2":
                        searchFld = "MBE_ADD2";
                        break;
                    case "VAT Reg":
                        searchFld = "MBE_TAX_NO";
                        break;
                    case "Passport No":
                        searchFld = "MBE_PP_NO";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_CUS_SEARCH_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[6] = new OracleParameter("p_ctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _custype;
                (param[7] = new OracleParameter("p_jobno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = jobno;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSTOMERJOBFILTERED", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_CUS_SEARCH_HEAD>(_dtResults, MST_CUS_SEARCH_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<JOB_NUM_SEARCH> getJobPouchSearchDateFiltered(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string com)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Pouch No":
                        searchFld = "CODE";
                        break;
                    case "Job No":
                        searchFld = "JOBNO";
                        break;
                    case "Date":
                        searchFld = "DATE_";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                
                List<JOB_NUM_SEARCH> itmList = new List<JOB_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_LGT_JOB_POUCH_DF", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_dtResults, JOB_NUM_SEARCH.ConverterJob);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<JOB_NUM_SEARCH> GetJobNumberInClose(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "JB_JB_DT";
                        break;
                    case "Pouch No":
                        searchFld = "JB_POUCH_NO";
                        break;
                    case "Status":
                        searchFld = "JB_STUS";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<JOB_NUM_SEARCH> itmList = new List<JOB_NUM_SEARCH>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[6] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[7] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_LGT_JOBFORCLOSE", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<JOB_NUM_SEARCH>(_dtResults, JOB_NUM_SEARCH.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SEARCH_INVOICE> getInvoiceNoDateFiltered(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODF", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SEARCH_INVOICE> getInvoiceNoDf(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODF", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SEARCH_INVOICE> getInvoiceNoDfApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODFAPP", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SEARCH_INVOICE> getInvoiceNoDfNew(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc, string hbl)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param[8] = new OracleParameter("p_hbl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hbl;               
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODF_NEW", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //non pc by dilshan on 10/04/2018
        public List<SEARCH_INVOICE> getInvoiceNoDfNonPC(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODF_NONPC", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //non pc approval by dilshan on 10/08/2018
        public List<SEARCH_INVOICE> getInvoiceNoDfNonPCApp(DateTime _fromDate, DateTime _toDate, string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string pc)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "CODE":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Code":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Invoice No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Debit Note No":
                        searchFld = "TIH_INV_NO";
                        break;
                    case "Job No":
                        searchFld = "JB_JB_NO";
                        break;
                    case "Date":
                        searchFld = "TIH_INV_DT";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<SEARCH_INVOICE> itmList = new List<SEARCH_INVOICE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[6] = new OracleParameter("p_frmdte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromDate;
                (param[7] = new OracleParameter("p_todte", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_INV_NODF_NONPCAPP", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<SEARCH_INVOICE>(_dtResults, SEARCH_INVOICE.ConverterInvRef);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_COST_ELEMENT_SEARCH> getRevenueElements(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            //string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string search = (!string.IsNullOrEmpty(searchVal)) ? searchVal + "%" : "";
            switch (searchFld)
            {
                case "Cost Code":
                    searchFld = "MCE_CD";
                    break;
                case "Description":
                    searchFld = "MCE_DESC";
                    break;
                case "Account Code":
                    searchFld = "MCE_ACC_CD";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COST_ELEMENT_SEARCH> req = new List<MST_COST_ELEMENT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "sp_search_revnelement", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<MST_COST_ELEMENT_SEARCH>(_dtResults, MST_COST_ELEMENT_SEARCH.Converter);
            }
            return req;
        }




        public List<PAY_TP_SEARCH> getPaymentTypes(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "SAPT_CD";
                    break;
                case "Description":
                    searchFld = "SAPT_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<PAY_TP_SEARCH> req = new List<PAY_TP_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_PAYTYP_DET", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<PAY_TP_SEARCH>(_dtResults, PAY_TP_SEARCH.Converter);
            }
            return req;
        }

        public List<USER_SEARCH> getUserDetails(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "SE_USR_ID";
                    break;
                case "Name":
                    searchFld = "SE_USR_NAME";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<USER_SEARCH> req = new List<USER_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;

            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_SEARCH_USER_DET", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                req = DataTableExtensions.ToGenericList<USER_SEARCH>(_dtResults, USER_SEARCH.Converter);
            }
            return req;
            
        }


        public List<MST_USERROLEID_SEARCH> getUserRoleID(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "ID":
                        searchFld = "ID";
                        break;
                    case "Description":
                        searchFld = "Description";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_USERROLEID_SEARCH> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRoleID", "SP_GET_USER_ROLES", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<MST_USERROLEID_SEARCH>(_dtResults, MST_USERROLEID_SEARCH.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_USERS> getUserList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "User ID":
                        searchFld = "ID";
                        break;
                    case "Name":
                        searchFld = "Name";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_USERS> userlist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblUserList", "SP_GET_USER_LIST", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    userlist = DataTableExtensions.ToGenericList<MST_USERS>(_dtResults, MST_USERS.converter);
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<MST_DEPARTMENT> getDeptList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "ID":
                        searchFld = "ID";
                        break;
                    case "Description":
                        searchFld = "Description";
                        break;
                    case "Head Of Department":
                        searchFld = "HOD";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_DEPARTMENT> userlist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblDeptList", "SP_GET_DEPT_LIST", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    userlist = DataTableExtensions.ToGenericList<MST_DEPARTMENT>(_dtResults, MST_DEPARTMENT.Converter);
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_DESIGNATION> getDesignationtList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "ID":
                        searchFld = "ID";
                        break;
                    case "Description":
                        searchFld = "Description";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<MST_DESIGNATION> userlist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblDesigList", "SP_GET_DESIGNATION_LIST", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    userlist = DataTableExtensions.ToGenericList<MST_DESIGNATION>(_dtResults, MST_DESIGNATION.Converter);
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_COM_SEARCH> getCompanySet(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "ID":
                    searchFld = "ID";
                    break;
                case "Description":
                    searchFld = "Description";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_COM_SEARCH> profCenters = new List<MST_COM_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblCom", "SP_GET_COMPANY_LIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                profCenters = DataTableExtensions.ToGenericList<MST_COM_SEARCH>(_dtResults, MST_COM_SEARCH.Converter);
            }
            return profCenters;
        }


    }
}
