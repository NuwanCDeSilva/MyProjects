using FF.BusinessObjects;
using FF.BusinessObjects.Genaral;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.BusinessObjects.Search;
using FF.BusinessObjects.Sales;


namespace FF.DataAccessLayer.BaseDAL
{
    public class GenaralDAL : BaseDAL
    { 
         
        /// <summary>
        /// added by isuru 2017/05/25
        /// get customer type details
        /// </summary>
        /// <returns></returns>
        public List<MST_COUNTRY> getCustomerCountry()
        {
            List<MST_COUNTRY> result = new List<MST_COUNTRY>();

            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("cus_typ", "SP_GET_CUSTOMERCOUNTRY_DETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_COUNTRY>(_dtResults, MST_COUNTRY.Converter);
            }
            return result;
        }

        /// <summary>
        /// Isuru 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerPassPNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Pass Code":
                        searchFld = "MBE_PP_NO";
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
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSPASSPNO_DET", CommandType.StoredProcedure, false, param);

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
        /// Isuru 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerDLNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Dl Code":
                        searchFld = "MBE_DL_NO";
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
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSDL_DET", CommandType.StoredProcedure, false, param);

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
        /// Isuru 2017/05/26
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerBRNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "BR Code":
                        searchFld = "MBE_BR_NO";
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
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSBRNO_DET", CommandType.StoredProcedure, false, param);

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
        /// Isuru 2017/05/27
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getCustomerTelNo(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Tel Code":
                        searchFld = "MBE_TEL";
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
                DataTable _dtResults = QueryDataTable("tblCus", "1", CommandType.StoredProcedure, false, param);

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
        /// Isuru 2017/05/31
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<cus_details> getcustomernicno(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Nic No":
                        searchFld = "MBE_TEL";
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
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSNIC_DET", CommandType.StoredProcedure, false, param);

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
        /// Isuru 2017/05/31
        /// </summary>
        /// <param name="pgeNum"></param>
        /// <param name="pgeSize"></param>
        /// <param name="searchFld"></param>
        /// <param name="searchVal"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<mst_bus_entity_tp> getcustomertype(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            try
            {
                string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
                switch (searchFld)
                {
                    case "Cus Typ":
                        searchFld = "MET_DESC";
                        break;
                    default:
                        searchFld = "";
                        break;
                }
                List<mst_bus_entity_tp> itmList = new List<mst_bus_entity_tp>();
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblCus", "SP_SEARCH_CUSTYP_DET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<mst_bus_entity_tp>(_dtResults, mst_bus_entity_tp.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //Subodana 2017-06-05
        public List<MainServices> GetMainServicesCodes()
        {
            try
            {
                List<MainServices> itmList = new List<MainServices>();
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_LOGI_GET_SERVICES", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<MainServices>(_dtResults, MainServices.Converter);
                }
                return itmList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-06-05
        public Int32 SaveJobserHDR(trn_jb_hdr _hdr, out int _seqNo)
        {
            _seqNo = -1;
            OracleParameter[] param = new OracleParameter[18];
            (param[0] = new OracleParameter("P_JB_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_com_cd;
            (param[1] = new OracleParameter("P_JB_JB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_jb_no;
            (param[2] = new OracleParameter("P_JB_JB_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Jb_jb_dt;
            (param[3] = new OracleParameter("P_JB_POUCH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_pouch_no;
            (param[4] = new OracleParameter("P_JB_SBU_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_sbu_cd;
            (param[5] = new OracleParameter("P_JB_MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_mos_cd;
            (param[6] = new OracleParameter("P_JB_TOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_tos_cd;
            (param[7] = new OracleParameter("P_JB_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_rmk;
            (param[8] = new OracleParameter("P_JB_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_stus;
            (param[9] = new OracleParameter("P_JB_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _hdr.Jb_stage;
            (param[10] = new OracleParameter("P_JB_SALES_EX_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_sales_ex_cd;
            (param[11] = new OracleParameter("P_JB_OTH_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_oth_ref;
            (param[12] = new OracleParameter("P_JB_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_cre_by;
            (param[13] = new OracleParameter("P_JB_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Jb_cre_dt;
            (param[14] = new OracleParameter("P_JB_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Jb_mod_by;
            (param[15] = new OracleParameter("P_JB_MOD_DE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Jb_mod_de;
            param[16] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            param[17] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_SER_JOBHDR", CommandType.StoredProcedure, param);
            if (result > 0) _seqNo = Convert.ToInt32(param[16].Value.ToString());
            return result;
        }
        //SUBODANA 2017/06/05
        public int SaveJobServiceCustormer(trn_jb_cus_det CUS)
        {
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_JC_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_seq_no;
            (param[1] = new OracleParameter("P_JC_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_cus_cd;
            (param[2] = new OracleParameter("P_JC_CUS_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_cus_tp;
            (param[3] = new OracleParameter("P_JC_EXE_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_exe_cd;
            (param[4] = new OracleParameter("P_JC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_cre_by;
            (param[5] = new OracleParameter("P_JC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = CUS.Jc_cre_dt;
            (param[6] = new OracleParameter("P_JC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CUS.Jc_cre_by;
            (param[7] = new OracleParameter("P_JC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = CUS.Jc_mod_dt;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_SER_JOBCUSTMER", CommandType.StoredProcedure, param);
            return result;
        }
        //SUBODANA 2017/06/06
        public int SaveJobServiceDetails(trn_job_serdet details)
        {
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("P_JS_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_SEQ_NO;
            (param[1] = new OracleParameter("P_JS_LINE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.JS_LINE_NO;
            (param[2] = new OracleParameter("P_JS_SER_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_SER_TP;
            (param[3] = new OracleParameter("P_JS_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_PC;
            (param[4] = new OracleParameter("P_JS_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_RMK;
            (param[5] = new OracleParameter("P_JS_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_CRE_BY;
            (param[6] = new OracleParameter("P_JS_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = details.JS_CRE_DT;
            (param[7] = new OracleParameter("P_JS_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_MOD_BY;
            (param[8] = new OracleParameter("P_JS_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = details.JS_MOD_DT;
            (param[9] = new OracleParameter("P_JS_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.JS_CUS_CD;
            param[10] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_SER_JOBDETAILS", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-06-07
        public int DeleteSerJobDetails(string seq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_REMOVE_SER_DET", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-06-07
        public List<PendingServiceRequest> GetPendingJobRequse(string Com)
        {
                List<PendingServiceRequest> itmList = new List<PendingServiceRequest>();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOBREQ", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    itmList = DataTableExtensions.ToGenericList<PendingServiceRequest>(_dtResults, PendingServiceRequest.Converter);
                }
                return itmList;
        }
        //subodana 2017-06-19
        public List<cus_details> GetCustormerdata(string Com, string cuscode)
        {
            List<cus_details> itmList = new List<cus_details>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CUSCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CUSTORMER_DET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<cus_details>(_dtResults, cus_details.Converter2);
            }
            return itmList;
        }

        //Dilshan 2017-09-11
        public List<cus_details> GetCountryTown(string Com, string concode)
        {
            List<cus_details> itmList = new List<cus_details>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CONCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = concode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_CountryTown", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                //itmList = DataTableExtensions.ToGenericList<cus_details>(_dtResults, cus_details.Converter2);
            }
            return itmList;
        }
        //Dilshan 04/09/2017
        public List<MST_EMP> GetEmployeedata(string Com, string empcode)
        {
            List<MST_EMP> itmList = new List<MST_EMP>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_epf", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empcode;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_EMPDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<MST_EMP>(_dtResults, MST_EMP.Converter);
            }
            return itmList;
        }

        public List<MST_VESSEL> GetVessaldata(string Com, string vslcode)
        {
            List<MST_VESSEL> itmList = new List<MST_VESSEL>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_vslcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = vslcode;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_vessaldetails", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<MST_VESSEL>(_dtResults, MST_VESSEL.Converter);
            }
            return itmList;
        }
        public List<MST_VESSEL> GetCostdata(string Com, string eleCode)
        {
            List<MST_VESSEL> itmList = new List<MST_VESSEL>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_elecd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = eleCode;
            //(param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_COSTELEDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<MST_VESSEL>(_dtResults, MST_VESSEL.Converter1);
            }
            return itmList;
        }
        public List<MST_VESSEL> GetPortdata(string Com, string prtCode)
        {
            List<MST_VESSEL> itmList = new List<MST_VESSEL>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_prtCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prtCode;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "sp_get_portrefdetails", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<MST_VESSEL>(_dtResults, MST_VESSEL.Converter2);
            }
            return itmList;
        }

        public List<Mst_empcate> Get_mst_empcate()
        {
            List<Mst_empcate> result = new List<Mst_empcate>();

            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_tour_get_mst_empcate", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Mst_empcate>(_dtResults, Mst_empcate.Converter);
            }
            return result;
        }

        public Int32 Check_Employeeepf(string epf)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = epf;
            param[1] = new OracleParameter("C_OUT", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_check_employeeepf", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 Check_vessal(string code)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[1] = new OracleParameter("C_OUT", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_check_vessalcode", CommandType.StoredProcedure, param);
            return effects;
        }
        //dilshan 08/09/2017
        public Int32 Check_costele(string code)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[1] = new OracleParameter("C_OUT", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_CHECK_COSTELECODE", CommandType.StoredProcedure, param);
            return effects;
        }
        //dilshan 08/09/2017
        public Int32 Check_port(string code)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            param[1] = new OracleParameter("C_OUT", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_CHECK_PORTCODE", CommandType.StoredProcedure, param);
            return effects;
        }
        public MasterAutoNumber GetAutoNumber(string _module, Int32 _direction, string _startChar, string _catType, string _catCode, DateTime _modifyDate, Int32 _year)
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
        /*
         * Sanjaya 2017-06-19 
         */
        public string SaveJobRequest(trn_req_hdr request)
        {
            OracleParameter[] param = new OracleParameter[15];
            (param[0] = new OracleParameter("P_RQ_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_com_cd;
            (param[1] = new OracleParameter("P_RQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_no;
            (param[2] = new OracleParameter("P_RQ_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.Rq_dt;
            (param[3] = new OracleParameter("P_RQ_POUCH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_pouch_no;
            (param[4] = new OracleParameter("P_RQ_SBU_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_sbu_cd;
            (param[5] = new OracleParameter("P_RQ_MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_mos_cd;
            (param[6] = new OracleParameter("P_RQ_TOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_tos_cd;
            (param[7] = new OracleParameter("P_RQ_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_rmk;
            (param[8] = new OracleParameter("P_RQ_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_stus;
            (param[9] = new OracleParameter("P_RQ_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = request.Rq_stage;
            (param[10] = new OracleParameter("P_RQ_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_cre_by;
            (param[11] = new OracleParameter("P_RQ_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.Rq_cre_dt;
            (param[12] = new OracleParameter("P_RQ_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = request.Rq_mod_by;
            (param[13] = new OracleParameter("P_RQ_MOD_DE", OracleDbType.Date, null, ParameterDirection.Input)).Value = request.Rq_mod_de;
            param[14] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_INSERT_TRN_REQ_HDR", CommandType.StoredProcedure, param);
            return result.ToString();
        }
        //subodana 2017-06-21
        public List<PendingServiceRequest> GetPendingJobRequstData(string Com, string Reqno)
        {
            List<PendingServiceRequest> itmList = new List<PendingServiceRequest>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_REQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Reqno;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOBREQDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<PendingServiceRequest>(_dtResults, PendingServiceRequest.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-22
        public List<trn_req_serdet> GetPendingJobserdata(string Seq)
        {
            List<trn_req_serdet> itmList = new List<trn_req_serdet>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOBSERDATA", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_req_serdet>(_dtResults, trn_req_serdet.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-22
        public List<trn_req_cus_det> GetPendingJobcusdata(string Seq)
        {
            List<trn_req_cus_det> itmList = new List<trn_req_cus_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PENDING_JOBCUSDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_req_cus_det>(_dtResults, trn_req_cus_det.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-22
        public List<trn_jb_hdr> GetJobHdr(string doc)
        {
            List<trn_jb_hdr> itmList = new List<trn_jb_hdr>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOB_HDR", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_jb_hdr>(_dtResults, trn_jb_hdr.Converter);
            }
            return itmList;
        }
        public List<trn_jb_hdr> GetJobHdrbypouch(string poch)
        {
            List<trn_jb_hdr> itmList = new List<trn_jb_hdr>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_POUCH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = poch;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOB_HDRBYPOUCH", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_jb_hdr>(_dtResults, trn_jb_hdr.Converter);
            }
            return itmList;
        }

        //subodana 2017-06-22
        public List<trn_job_serdet> GetJobServicesdetails(string seq)
        {
            List<trn_job_serdet> itmList = new List<trn_job_serdet>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOB_SER", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_job_serdet>(_dtResults, trn_job_serdet.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-22
        public List<trn_jb_cus_det> GetJobCustomerDetails(string seq)
        {
            List<trn_jb_cus_det> itmList = new List<trn_jb_cus_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOB_CUS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_jb_cus_det>(_dtResults, trn_jb_cus_det.Converter);
            }
            return itmList;
        }


        //Sanjaya 2017-06-24
        public int SaveNewCustomer(CustomerSearchObject customerObject)
        {
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("P_CUS_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_com;
            (param[1] = new OracleParameter("P_CUS_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_cd;
            (param[2] = new OracleParameter("P_CUS_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_tp;
            (param[3] = new OracleParameter("P_CUS_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_name;
            (param[4] = new OracleParameter("P_CUS_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_add1;
            (param[5] = new OracleParameter("P_CUS_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_add2;
            (param[6] = new OracleParameter("P_CUS_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_mob;
            (param[7] = new OracleParameter("P_CUS_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_email;
            (param[8] = new OracleParameter("P_CUS_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_wr_com_name;
            (param[9] = new OracleParameter("P_CUS_COUNTRY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_country_cd;
            (param[10] = new OracleParameter("P_CUS_CREATED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customerObject.Mbe_cre_by;
            (param[11] = new OracleParameter("P_CUS_CREATED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = customerObject.Mbe_cre_dt;
            param[12] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_CUSTOMER", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-06-26
        public Int32 SaveBLHDR(trn_bl_header _hdr, out int _seqNo)
        {
            _seqNo = -1;
            OracleParameter[] param = new OracleParameter[71];
            (param[0] = new OracleParameter("P_BL_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_com_cd;
            (param[1] = new OracleParameter("P_BL_SBU_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_sbu_cd;
            (param[2] = new OracleParameter("P_BL_MOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_mos_cd;
            (param[3] = new OracleParameter("P_BL_TOS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_tos_cd;
            (param[4] = new OracleParameter("P_BL_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_doc_no;
            (param[5] = new OracleParameter("P_BL_D_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_d_doc_no;
            (param[6] = new OracleParameter("P_BL_H_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_h_doc_no;
            (param[7] = new OracleParameter("P_BL_M_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_m_doc_no;
            (param[8] = new OracleParameter("P_BL_JOB_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_job_no;
            (param[9] = new OracleParameter("P_BL_POUCH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_pouch_no;
            (param[10] = new OracleParameter("P_BL_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_cus_cd;
            (param[11] = new OracleParameter("P_BL_SHIP_LINE_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ship_line_cd;
            (param[12] = new OracleParameter("P_BL_SHIP_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ship_line;
            (param[13] = new OracleParameter("P_BL_AGENT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_agent_cd;
            (param[14] = new OracleParameter("P_BL_EST_TIME_ARR", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Bl_est_time_arr;
            (param[15] = new OracleParameter("P_BL_EST_TIME_DEP", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Bl_est_time_dep;
            (param[16] = new OracleParameter("P_BL_LOAD_IN_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_load_in_tp;
            (param[17] = new OracleParameter("P_BL_LOAD_OUT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_load_out_tp;
            (param[18] = new OracleParameter("P_BL_PRINT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_print_cd;
            (param[19] = new OracleParameter("P_BL_DOC_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_doc_tp;
            (param[20] = new OracleParameter("P_BL_DOC_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Bl_doc_dt;
            (param[21] = new OracleParameter("P_BL_REF_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ref_no;
            (param[22] = new OracleParameter("P_BL_BL_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_bl_tp;
            (param[23] = new OracleParameter("P_BL_SHIPPER_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_shipper_cd;
            (param[24] = new OracleParameter("P_BL_SHIPPER_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_shipper_name;
            (param[25] = new OracleParameter("P_BL_SHIPPER_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_shipper_add1;
            (param[26] = new OracleParameter("P_BL_SHIPPER_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_shipper_add2;
            (param[27] = new OracleParameter("P_BL_CONSIGNEE_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_consignee_cd;
            (param[28] = new OracleParameter("P_BL_CONSIGNEE_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_consignee_name;
            (param[29] = new OracleParameter("P_BL_CONSIGNEE_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_consignee_add1;
            (param[30] = new OracleParameter("P_BL_CONSIGNEE_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_consignee_add2;
            (param[31] = new OracleParameter("P_BL_NTFY_PARTY_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ntfy_party_cd;
            (param[32] = new OracleParameter("P_BL_NTFY_PARTY_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ntfy_party_name;
            (param[33] = new OracleParameter("P_BL_NTFY_PARTY_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ntfy_party_add1;
            (param[34] = new OracleParameter("P_BL_NTFY_PARTY_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_ntfy_party_add2;
            (param[35] = new OracleParameter("P_BL_DEL_AGENT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_agent_cd;
            (param[36] = new OracleParameter("P_BL_DEL_AGENT_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_del_agent_name;
            (param[37] = new OracleParameter("P_BL_DEL_AGENT_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_del_agent_add1;
            (param[38] = new OracleParameter("P_BL_DEL_AGENT_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_del_agent_add2;
            (param[39] = new OracleParameter("P_BL_VOAGE_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_voage_no;
            (param[40] = new OracleParameter("P_BL_VOAGE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.Bl_voage_dt;
            (param[41] = new OracleParameter("P_BL_VESSAL_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_vessal_no;
            (param[42] = new OracleParameter("P_BL_PALCE_REC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_palce_rec;
            (param[43] = new OracleParameter("P_BL_PORT_LOAD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_port_load;
            (param[44] = new OracleParameter("P_BL_PORT_DISCHARGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_port_discharge;
            (param[45] = new OracleParameter("P_BL_PALCE_DEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_palce_del;
            (param[46] = new OracleParameter("P_BL_FREIGHT_CHARG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _hdr.Bl_freight_charg;
            (param[47] = new OracleParameter("P_BL_FREIGHT_PAYBLE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _hdr.Bl_freight_payble;
            (param[48] = new OracleParameter("P_BL_NO_OF_BOL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _hdr.Bl_no_of_bol;
            (param[49] = new OracleParameter("P_BL_RECV_SHIP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_recv_ship;
            (param[50] = new OracleParameter("P_BL_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.Bl_rmk;
            (param[51] = new OracleParameter("P_BL_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _hdr.Bl_act;
            (param[52] = new OracleParameter("P_BL_EXPIRES_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.bl_expires_dt;
            (param[53] = new OracleParameter("P_BL_MANUAL_D_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_manual_d_ref;
            (param[54] = new OracleParameter("P_BL_MANUAL_H_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_manual_h_ref;
            (param[55] = new OracleParameter("P_BL_MANUAL_M_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_manual_m_ref;
            (param[56] = new OracleParameter("P_BL_M_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_m_seq_no;
            (param[57] = new OracleParameter("P_BL_PACK_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_pack_uom;
            (param[58] = new OracleParameter("P_BL_TERMINAL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_terminal;
            (param[59] = new OracleParameter("P_BL_DO_PRINT", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _hdr.bl_do_print;
            (param[60] = new OracleParameter("P_BL_PAY_MODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_pay_mode;
            (param[61] = new OracleParameter("P_BL_VSL_OPER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_vsl_oper;
            (param[62] = new OracleParameter("P_BL_CNTR_OPER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_cntr_oper;
            (param[63] = new OracleParameter("P_BL_TRANS_MODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_trans_mode;
            (param[64] = new OracleParameter("P_BL_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_cre_by;
            (param[65] = new OracleParameter("P_BL_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.bl_cre_dt;
            (param[66] = new OracleParameter("P_BL_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_mod_by;
            (param[67] = new OracleParameter("P_BL_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _hdr.bl_mod_dt;
            (param[68] = new OracleParameter("P_BL_OR_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _hdr.bl_or_seq;
            param[69] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            param[70] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_DRBL_HDR", CommandType.StoredProcedure, param);
            if (result > 0) _seqNo = Convert.ToInt32(param[69].Value.ToString()); 
            return result;
        }
        //SUBODANA 2017/06/27
        public int SaveDrafBLDetails(trn_bl_det details)
        {
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("P_BLD_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_seq_no;
            (param[1] = new OracleParameter("P_BLD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.bld_seq_no;
            (param[2] = new OracleParameter("P_BLD_MARK_NOS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_mark_nos;
            (param[3] = new OracleParameter("P_BLD_PACKAGE_NOS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_package_nos;
            (param[4] = new OracleParameter("P_BLD_DESC_GOODS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_desc_goods;
            (param[5] = new OracleParameter("P_BLD_GRS_WEIGHT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.bld_grs_weight;
            (param[6] = new OracleParameter("P_BLD_GRS_WEIGHT_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_grs_weight_uom;
            (param[7] = new OracleParameter("P_BLD_NET_WEIGHT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.bld_grs_weight;
            (param[8] = new OracleParameter("P_BLD_NET_WEIGHT_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_net_weight_uom;
            (param[9] = new OracleParameter("P_BLD_MEASURE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = details.bld_measure;
            (param[10] = new OracleParameter("P_BLD_MEASURE_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_measure_uom;
            (param[11] = new OracleParameter("P_BLD_PACKAGE_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.bld_package_tp;
            param[12] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_DRBL_DET", CommandType.StoredProcedure, param);
            return result;
        }
        //SUBODANA 2017/06/27
        public int SaveDrafBLContainerDetails(trn_bl_cont_det details)
        {
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_BLCT_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_seq_no;
            (param[1] = new OracleParameter("P_BLCT_BL_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_bl_doc;
            (param[2] = new OracleParameter("P_BLCT_CONT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_cont_no;
            (param[3] = new OracleParameter("P_BLCT_SEAL_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_seal_no;
            (param[4] = new OracleParameter("P_BLCT_CON_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.blct_con_tp;
            (param[5] = new OracleParameter("P_BLCT_FULLY_EMPTY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_fully_empty;
            (param[6] = new OracleParameter("P_BLCT_PACK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = details.Blct_pack;
            (param[7] = new OracleParameter("P_BLCT_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = details.Blct_line;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_DRBL_CON", CommandType.StoredProcedure, param);
            return result;
        }
        //Sanjaya 2017-06-26
        public int SaveNewCustomerGroup(mst_busentity_grup busentityGrup)
        {
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("P_CUS_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_cd;
            (param[1] = new OracleParameter("P_CUS_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_name;
            (param[2] = new OracleParameter("P_CUS_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_add1;
            (param[3] = new OracleParameter("P_CUS_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_add2;
            (param[4] = new OracleParameter("P_CUS_COUNTRY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_country_cd;
            (param[5] = new OracleParameter("P_CUS_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_mob;
            (param[6] = new OracleParameter("P_CUS_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_email;
            (param[7] = new OracleParameter("P_CUS_CREATED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_cre_by;
            (param[8] = new OracleParameter("P_CUS_CREATED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_cre_dt;
            (param[9] = new OracleParameter("P_CUS_MODIFIED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_mod_by;
            (param[10] = new OracleParameter("P_CUS_MODIFIED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = busentityGrup.Mbg_mod_dt;
            param[11] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_SAVE_CUSTOMER_GROUP", CommandType.StoredProcedure, param);
            return result;
        }
        //Sanjaya 2017-06-27
        public int SaveRequestDetails(trn_req_serdet reqSerdet)
        {
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("P_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_seq_no;
            (param[1] = new OracleParameter("P_LINE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = reqSerdet.rs_line_no;
            (param[2] = new OracleParameter("P_MAIN_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_ser_tp;
            (param[3] = new OracleParameter("P_REMARK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_rmk;
            (param[4] = new OracleParameter("P_CREATED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_cre_by;
            (param[5] = new OracleParameter("P_CREATED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = reqSerdet.rs_cre_dt;
            (param[6] = new OracleParameter("P_MODIFIED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_mod_by;
            (param[7] = new OracleParameter("P_MODIFIED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = reqSerdet.rs_mod_dt;
            (param[8] = new OracleParameter("P_CUS_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_cus_cd;
            (param[9] = new OracleParameter("P_SUB_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqSerdet.rs_mser_tp;
            param[10] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_INSERT_TRN_REQ_SERDET", CommandType.StoredProcedure, param);
            return result;
        }
        //Sanjaya 2017-06-27
        public int SaveRequesCustomertDetails(trn_req_cus_det reqCusDet)
        {
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("P_SEQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqCusDet.Rc_seq_no;
            (param[1] = new OracleParameter("P_CUS_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqCusDet.Rc_cus_cd;
            (param[2] = new OracleParameter("P_CUS_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqCusDet.Rc_cus_tp;
            (param[3] = new OracleParameter("P_CREATED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqCusDet.Rc_cre_by;
            (param[4] = new OracleParameter("P_CREATED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = reqCusDet.Rc_cre_dt;
            (param[5] = new OracleParameter("P_MODIFIED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reqCusDet.Rc_mod_by;
            (param[6] = new OracleParameter("P_MODIFIED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = reqCusDet.Rc_mod_dt;
            param[7] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_INSERT_TRN_REQ_CUS_DET", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-06-28
        public int DeleteBLContdata(string seq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_REMOVE_CONT_DET", CommandType.StoredProcedure, param);
            return result;
        }
        //subodana 2017-06-28
        public List<trn_bl_header> GetBLHdr(string doc,string com)
        {
            List<trn_bl_header> itmList = new List<trn_bl_header>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_BLNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DR_BLDATA", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_bl_header>(_dtResults, trn_bl_header.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-28
        public List<trn_bl_cont_det> GetBLContainer(string seq)
        {
            List<trn_bl_cont_det> itmList = new List<trn_bl_cont_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DR_BLCONT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_bl_cont_det>(_dtResults, trn_bl_cont_det.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-28
        public List<trn_bl_det> GetBLitemdetails(string seq)
        {
            List<trn_bl_det> itmList = new List<trn_bl_det>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DR_BLITEMDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_bl_det>(_dtResults, trn_bl_det.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-29
        public List<BL_DOC_NO> GetBLDocNo(string type, string searchtype, string docno)
        {
            List<BL_DOC_NO> itmList = new List<BL_DOC_NO>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[1] = new OracleParameter("P_SEARCH_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchtype;
            (param[2] = new OracleParameter("P_DOC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_DOC_NO", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<BL_DOC_NO>(_dtResults, BL_DOC_NO.Converter);
            }
            return itmList;
        }
        //subodana 2017-06-29
        public List<GET_CUS_BASIC_DATA> GetCustormerBasicData(string code, string com, string type)
        {
            List<GET_CUS_BASIC_DATA> itmList = new List<GET_CUS_BASIC_DATA>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_CUSTORMER_NAMES", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<GET_CUS_BASIC_DATA>(_dtResults, GET_CUS_BASIC_DATA.Converter);
            }
            return itmList;
        }
        //subodana 2017-07-04
        public List<EntityType> GetJobEntity()
        {
            List<EntityType> itmList = new List<EntityType>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ENT_TP", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<EntityType>(_dtResults, EntityType.Converter);
            }
            return itmList;
        }

        public List<MST_CUR> GetAllCurrency(string _currencyCode)
        {
            List<MST_CUR> _masterType = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _currencyCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblMasterCurrency", "sp_getcurrency", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _masterType = DataTableExtensions.ToGenericList<MST_CUR>(_dtResults, MST_CUR.Converter);
            }
            return _masterType;
        }

        public TRN_MOD_MAX_APPLVL getMaxAppLvlPermission(string modcd, string com)
        {
            TRN_MOD_MAX_APPLVL data = new TRN_MOD_MAX_APPLVL();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_mod", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = modcd;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_MODULEMAXAPPLVL", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                data = DataTableExtensions.ToGenericList<TRN_MOD_MAX_APPLVL>(_dtResults, TRN_MOD_MAX_APPLVL.Converter)[0];
            }
            return data;
        }
        public List<MST_COUNTRY> getCountryDetails(string countryCd)
        {
            List<MST_COUNTRY> result = new List<MST_COUNTRY>();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_CUTYCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = countryCd;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_country", "SP_GET_COUNTRYDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_COUNTRY>(_dtResults, MST_COUNTRY.Converter);
            }
            return result;
        }
        public DataTable Get_DetBy_town(string town)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = town;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tbDet = new DataTable();
            DataTable tblDet = QueryDataTable("tbl", "SP_GET_DetBy_town", CommandType.StoredProcedure, false, param);

            return tblDet;
        }
        //subodana 2017-07-13
        public int UpdateReqStatus(string DOC, String job)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = DOC;
            (param[1] = new OracleParameter("P_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = job;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_PENDING_REQST", CommandType.StoredProcedure, param);
            return result;
        }

        //Sanjaya De Silva 2017-07-13

        public List<trn_jb_hdr> GetCustomerJobs(string cus_code)
        {
            List<trn_jb_hdr> itmList = new List<trn_jb_hdr>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_JOBNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cus_code;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_JOB_BY_CUS_CD", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                itmList = DataTableExtensions.ToGenericList<trn_jb_hdr>(_dtResults, trn_jb_hdr.Converter);
            }
            return itmList;
        }
        public DataTable getSubChannelDet(string _company, string _code)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_schnl", CommandType.StoredProcedure, false, param);

            return _itemTable;
        }

        /*Sanjaya De SIlva 2017-08-05*/

        public List<cus_invoices> getCustomerInvoices(string _cus_code)
        {
            List<cus_invoices> _invoices = new List<cus_invoices>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cus_code;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_INV_DETAILS_BY_CUS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _invoices = DataTableExtensions.ToGenericList<cus_invoices>(_dtResults, cus_invoices.Converter);
            }
            return _invoices;
        }

        public List<PortAgentDet> getPortDetails(DateTime fromdate, DateTime todate, string company)
        {
            try
            {
                List<PortAgentDet> det = new List<PortAgentDet>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
                (param[1] = new OracleParameter("p_tomdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PORTDETAILS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<PortAgentDet>(_dtResults, PortAgentDet.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getShipmentContainers(DateTime fromdate, DateTime todate, string company, string port, string agent)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = port;
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agent;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_CONTAINERSCOUNT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<BarChartDataPort> getPortTotal(DateTime frmdt, DateTime toDt, string company)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartDataPort> lst = new List<BarChartDataPort>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "PORT";
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_TOTALPORTAGENTDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                lst = DataTableExtensions.ToGenericList<BarChartDataPort>(_dtResults, BarChartDataPort.Converter);
            }
            return lst;
        }

        public List<BarChartDataAgent> getAgentTotal(DateTime frmdt, DateTime toDt, string company)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartDataAgent> lst = new List<BarChartDataAgent>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "AGENT";
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_TOTALPORTAGENTDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                lst = DataTableExtensions.ToGenericList<BarChartDataAgent>(_dtResults, BarChartDataAgent.Converter);
            }
            return lst;
        }
        //subodana 2017-09-1
        public int UpdateHouseBLMaster(string Housebl, String Masterbl)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_HOUSE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Housebl;
            (param[1] = new OracleParameter("P_MASTER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Masterbl;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            Int32 result = UpdateRecords("SP_UPDATE_HOUSEBL_REF", CommandType.StoredProcedure, param);
            return result;
        }

        public List<MST_TITLE> GetTitleList()
        {
            List<MST_TITLE> listTitle = new List<MST_TITLE>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblTitle", "SP_GET_TITLELIST", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                listTitle = DataTableExtensions.ToGenericList<MST_TITLE>(_dtResults, MST_TITLE.Converter);
            }
            return listTitle;
        }

        // Added by Chathura on 18-sep-2017
        public List<HBLSelectedData> GetHBLNumbersForMaster(string MBLno)
        {
            List<HBLSelectedData> list_hbl = new List<HBLSelectedData>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_master", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MBLno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblTitle", "SP_GETHBLSFORMBL", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                list_hbl = DataTableExtensions.ToGenericList<HBLSelectedData>(_dtResults, HBLSelectedData.Converter);
            }
            return list_hbl;
        }

        // Added by Chathura on 3-oct-2017
        public List<BLData> LoadBLDetails(string BLno)
        {
            List<BLData> list_hbl = new List<BLData>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_blno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BLno;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblTitle", "sp_load_bldetails", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                list_hbl = DataTableExtensions.ToGenericList<BLData>(_dtResults, BLData.Converter);
            }
            return list_hbl;
        }

        public List<CustomerBasicData> GetCustomerBasicDetails(string cuscode)
        {
            List<CustomerBasicData> list_hbl = new List<CustomerBasicData>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_cuscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblTitle", "sp_get_customerbasicdetails", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                list_hbl = DataTableExtensions.ToGenericList<CustomerBasicData>(_dtResults, CustomerBasicData.ConverterCusDetails);
            }
            return list_hbl;
        }

        public Int32 SaveReportErrorLog(string _erropt, string _errform, string _error, string _user)
        {//Dilshan 2018-03-05
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_err_option", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _erropt;
            (param[1] = new OracleParameter("p_err_form", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _errform;
            (param[2] = new OracleParameter("p_err_error", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _error;
            (param[3] = new OracleParameter("p_err_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_save_rpt_err_msg", CommandType.StoredProcedure, param);
        }
        public Int32 SaveSMSOut(OutSMS _smsout)
        {
            int _effect = 0;
            OracleParameter[] param = new OracleParameter[17];

            (param[0] = new OracleParameter("p_createtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Createtime;
            (param[1] = new OracleParameter("p_deletedtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Deletedtime;
            (param[2] = new OracleParameter("p_deliveredtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Deliveredtime;
            (param[3] = new OracleParameter("p_downloadtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Downloadtime;
            (param[4] = new OracleParameter("p_msg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msg;
            (param[5] = new OracleParameter("p_msgid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msgid;
            (param[6] = new OracleParameter("p_msgstatus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _smsout.Msgstatus;
            (param[7] = new OracleParameter("p_msgtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Msgtype;
            (param[8] = new OracleParameter("p_receivedtime", OracleDbType.Date, null, ParameterDirection.Input)).Value = _smsout.Receivedtime;
            (param[9] = new OracleParameter("p_receiver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Receiver;
            (param[10] = new OracleParameter("p_receiverphno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Receiverphno;
            (param[11] = new OracleParameter("p_refdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Refdocno;
            (param[12] = new OracleParameter("p_sender", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Sender;
            (param[13] = new OracleParameter("p_senderphno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.Senderphno;
            (param[14] = new OracleParameter("p_seqno", OracleDbType.Int64, null, ParameterDirection.Input)).Value = _smsout.Seqno;
            (param[15] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _smsout.comcode;
            param[16] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _effect = UpdateRecords("ems.sp_savesmsout", CommandType.StoredProcedure, param);

            return _effect;
        }


        public List<USER_ROLE> getUserRoleID(string company,string roleid)
        {
            try
            {
                List<USER_ROLE> cuslist = null;
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRoleIDDetails", "SP_GET_USER_ROLES_BYID", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<USER_ROLE>(_dtResults, USER_ROLE.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<UsersRoleData> getUserDetailsByRID(string company, string roleid)
        {
            try
            {
                List<UsersRoleData> cuslist = null;
                OracleParameter[] param = new OracleParameter[3];

                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblUserDetailsByID", "SP_GET_USER_DETAILS_BYROLEID", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<UsersRoleData>(_dtResults, UsersRoleData.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ROLE_MENU_BRID> getMenuDetailsByRID(string userid, string company, string roleid)
        {
            try
            {
                List<ROLE_MENU_BRID> cuslist = null;
                OracleParameter[] param = new OracleParameter[4];

                (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblMenuDetailsByID", "SP_GET_MENUS_BYROLEID", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<ROLE_MENU_BRID>(_dtResults, ROLE_MENU_BRID.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<OptionId_Details> getOptions(string company, string roleid)
        {
            try
            {
                List<OptionId_Details> optionist = null;
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_roleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(roleid);
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblOptionID", "SP_GET_OPTION_IDS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    optionist = DataTableExtensions.ToGenericList<OptionId_Details>(_dtResults, OptionId_Details.Converter);
                }
                return optionist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Save_Option_IDS(string activestatus, string company, string roleid, string optionid)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_stat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = activestatus;
                (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_roleid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = roleid;
                (param[3] = new OracleParameter("p_optid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = optionid;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_OPTION_ID", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USER_COMPANY_LIST> getUserListCom(string userid)
        {
            try
            {
                List<USER_COMPANY_LIST> optionist = null;
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblUserList", "SP_GET_USER_COMPANY_LIST", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    optionist = DataTableExtensions.ToGenericList<USER_COMPANY_LIST>(_dtResults, USER_COMPANY_LIST.Converter);
                }
                return optionist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MST_SHIPMENTS> getShipmentList(string company)
        {
            try
            {
                List<MST_SHIPMENTS> shiplist = null;
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblShipments", "SP_GET_SHIPMENT_IDS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    shiplist = DataTableExtensions.ToGenericList<MST_SHIPMENTS>(_dtResults, MST_SHIPMENTS.Converter);
                }
                return shiplist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update_Modeshipment_Details(string p_company, string p_modetype, string p_modedesc, string p_isactive, string p_choice)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_modetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_modetype;
                (param[2] = new OracleParameter("p_modedesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_modedesc;
                (param[3] = new OracleParameter("p_isactive", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_isactive;
                (param[4] = new OracleParameter("p_choice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_choice;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_UPDATE_MODESHIPMENT", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
