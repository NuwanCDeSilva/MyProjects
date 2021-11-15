using FF.BusinessObjects;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.ReptObj;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.DataAccessLayer.DashboardDAL
{
    public class DashboardDLL : BaseDAL
    {
        public MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode)
        {
            MasterLocation _locList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CompCode;
            (param[1] = new OracleParameter("p_loc_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _LocCode;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblloc", "pkg_dashboard.sp_get_location_by_code", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //_locList = DataTableExtensions.ToGenericList<MasterLocation>(_dtResults, MasterLocation.converter)[0];
                //Edit by Chamal 29/06/2012
                _locList = DataTableExtensions.ToGenericList<MasterLocation>(_dtResults, MasterLocation.ConverterTotal)[0];
            }
            return _locList;
        }

        public List<BMT_REF_HEAD> getBIToolProperties(string _searchValue, string _pageNum, string _pageSize, string _serachField, string _propertyType, string _module)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "BMR_COL_NM";
                        break;
                    case "Description":
                        _serachField = "BMR_COL_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }

                List<BMT_REF_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_colDescription", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[3] = new OracleParameter("p_serachField", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[4] = new OracleParameter("p_propertyType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _propertyType;
                (param[5] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_Search_BIToolPropertiesNew", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //dilshan on 13/06/2018
        public List<BMT_REF_HEAD> getBIToolInventory(string _searchValue, string _pageNum, string _pageSize, string _serachField, string _propertyType)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "BMR_COL_NM";
                        break;
                    case "Description":
                        _serachField = "BMR_COL_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }

                List<BMT_REF_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_colDescription", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[3] = new OracleParameter("p_serachField", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[4] = new OracleParameter("p_propertyType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _propertyType;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_Search_BIToolInventory", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<BMT_REF_HEAD> getBIToolImport(string _searchValue, string _pageNum, string _pageSize, string _serachField, string _propertyType)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "BMR_COL_NM";
                        break;
                    case "Description":
                        _serachField = "BMR_COL_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }

                List<BMT_REF_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_colDescription", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[3] = new OracleParameter("p_serachField", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[4] = new OracleParameter("p_propertyType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _propertyType;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_Search_BIToolImport", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<BRAND_MNGR_SEARCH_HEAD> getBrandManagers(string pgeNum, string pgeSize, string searchFld, string searchVal, string companies)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Manager Code":
                    searchFld = "ESEP_MANAGER_CD";
                    break;
                case "Emp Id":
                    searchFld = "MBA_EMP_ID";
                    break;
                case "Company":
                    searchFld = "ESEP_COM_CD";
                    break;
                case "Name":
                    searchFld = "ESEP_FIRST_NAME";
                    break;
                case "Epf":
                    searchFld = "ESEP_EPF";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<BRAND_MNGR_SEARCH_HEAD> crg = new List<BRAND_MNGR_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_companies", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = companies;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_BRANDMNGRS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BRAND_MNGR_SEARCH_HEAD>(_dtResults, BRAND_MNGR_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        //Added by Udesh 21-Jan-2019
        public List<SALES_MNGR_SEARCH_HEAD> getSalesManagers(string pgeNum, string pgeSize, string searchFld, string searchVal, string companies)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Manager Code":
                    searchFld = "ESEP_MANAGER_CD";
                    break;
                case "Company":
                    searchFld = "ESEP_COM_CD";
                    break;
                case "Name":
                    searchFld = "ESEP_FIRST_NAME";
                    break;
                case "Epf":
                    searchFld = "ESEP_EPF";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<SALES_MNGR_SEARCH_HEAD> crg = new List<SALES_MNGR_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_companies", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = companies;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_SALESMNGRS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<SALES_MNGR_SEARCH_HEAD>(_dtResults, SALES_MNGR_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        public List<BMT_REF_HEAD> LoadBIToolDetailsByName(string _columnName)
        {
            try
            {
                List<BMT_REF_HEAD> propertyList = null;
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_column_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _columnName;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_GET_BITOOLPROPERTY_DEATILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    propertyList = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return propertyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<BRAND_SEARCH_HEAD> getBrands(string pgeNum, string pgeSize, string searchFld, string searchVal, string brandMngr)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MB_CD";
                    break;
                case "Description":
                    searchFld = "MB_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<BRAND_SEARCH_HEAD> crg = new List<BRAND_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_brandMngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brandMngr;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_BRANDS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BRAND_SEARCH_HEAD>(_dtResults, BRAND_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        public List<BMT_SALE> getTargetAlignmentData(string selCompany, string Brands, string allbrnd, string allbrndmngr, DateTime startDate, DateTime endDate, string defby, string filterVal)
        {
            try
            {
                List<BMT_SALE> sale = new List<BMT_SALE>();
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selCompany;
                (param[1] = new OracleParameter("p_allbrnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = allbrnd;
                (param[2] = new OracleParameter("p_allbrndmngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = allbrndmngr;
                (param[3] = new OracleParameter("p_brands", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brands;
                (param[4] = new OracleParameter("p_startDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = startDate.ToString("d");
                (param[5] = new OracleParameter("p_endDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = endDate.ToString("d");
                (param[6] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
                (param[7] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterVal;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable data = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_BMTSALEDATA", CommandType.StoredProcedure, false, param);
                if (data.Rows.Count > 0)
                {
                    sale = DataTableExtensions.ToGenericList<BMT_SALE>(data, BMT_SALE.Converter);
                }
                return sale;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MST_BRND_ALLOC> getBrandsCodesForManagers(string BrandMngr)
        {
            List<MST_BRND_ALLOC> mngrbrnd = new List<MST_BRND_ALLOC>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mngrcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BrandMngr;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable data = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_BRDMANGRASGNBRANDS", CommandType.StoredProcedure, false, param);
            if (data.Rows.Count > 0)
            {
                mngrbrnd = DataTableExtensions.ToGenericList<MST_BRND_ALLOC>(data, MST_BRND_ALLOC.Converter);
            }
            return mngrbrnd;
        }

        public List<SARFORPD_SEARCH_HEAD> getTargetDates(string pgeNum, string pgeSize, string searchFld, string searchVal, string defby, string catdefon)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "SFP_PD_CD";
                    break;
                case "Description":
                    searchFld = "SFP_DESC";
                    break;
                case "From Date":
                    searchFld = "SFP_FRM_PD";
                    break;
                case "To Date":
                    searchFld = "SFP_TO_PD";
                    break;
                case "Calender Code":
                    searchFld = "SFP_CAL_CD";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<SARFORPD_SEARCH_HEAD> crg = new List<SARFORPD_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_catdefon", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = catdefon;
            (param[5] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_SARFORPD", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<SARFORPD_SEARCH_HEAD>(_dtResults, SARFORPD_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        public SAR_FOR_PD getTargetDateValues(string code, string defby, string catdefon, string calendcode)
        {
            SAR_FOR_PD pd = new SAR_FOR_PD();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[1] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
            (param[2] = new OracleParameter("p_catdefon", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = catdefon;
            (param[3] = new OracleParameter("p_calcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = calendcode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable data = QueryDataTable("tblcrg", "SP_GET_GET_SARPDVALUE", CommandType.StoredProcedure, false, param);
            if (data.Rows.Count > 0)
            {
                pd = DataTableExtensions.ToGenericList<SAR_FOR_PD>(data, SAR_FOR_PD.Converter)[0];
            }
            return pd;
        }

        //Randima 01-08-2016
        public List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            zone = (!String.IsNullOrWhiteSpace(zone)) ? (zone.ToUpper()) + "%" : "%";
            region = (!String.IsNullOrWhiteSpace(region)) ? (region.ToUpper()) + "%" : "%";
            area = (!String.IsNullOrWhiteSpace(area)) ? (area.ToUpper()) + "%" : "%";
            subChannel = (!String.IsNullOrWhiteSpace(subChannel)) ? (subChannel.ToUpper()) + "%" : "%";
            channel = (!String.IsNullOrWhiteSpace(channel)) ? (channel.ToUpper()) + "%" : "%";

            List<LOC_HIRCH_SEARCH_HEAD> crg = new List<LOC_HIRCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[7] = new OracleParameter("p_schannel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subChannel;
            (param[8] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[9] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[10] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_LOC_HIRCH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<LOC_HIRCH_SEARCH_HEAD>(_dtResults, LOC_HIRCH_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        //Dilshan 04-12-2017
        public List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy_new1(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";

            //company = company.Replace(",", "','");
            //company = "AAL','ABA','ABE','ABL";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            zone = (!String.IsNullOrWhiteSpace(zone)) ? (zone.ToUpper()) + "%" : "%";
            region = (!String.IsNullOrWhiteSpace(region)) ? (region.ToUpper()) + "%" : "%";
            area = (!String.IsNullOrWhiteSpace(area)) ? (area.ToUpper()) + "%" : "%";
            subChannel = (!String.IsNullOrWhiteSpace(subChannel)) ? (subChannel.ToUpper()) + "%" : "%";
            channel = (!String.IsNullOrWhiteSpace(channel)) ? (channel.ToUpper()) + "%" : "%";

            List<LOC_HIRCH_SEARCH_HEAD> crg = new List<LOC_HIRCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[7] = new OracleParameter("p_schannel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subChannel;
            (param[8] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[9] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[10] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_LOC_HIRCH_NEW1", CommandType.StoredProcedure, false, param);
            //DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_LOC_HIRCH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<LOC_HIRCH_SEARCH_HEAD>(_dtResults, LOC_HIRCH_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        //Dilshan 29-11-2017
        public List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchy_new(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            zone = (!String.IsNullOrWhiteSpace(zone)) ? (zone.ToUpper()) + "%" : "%";
            region = (!String.IsNullOrWhiteSpace(region)) ? (region.ToUpper()) + "%" : "%";
            area = (!String.IsNullOrWhiteSpace(area)) ? (area.ToUpper()) + "%" : "%";
            subChannel = (!String.IsNullOrWhiteSpace(subChannel)) ? (subChannel.ToUpper()) + "%" : "%";
            channel = (!String.IsNullOrWhiteSpace(channel)) ? (channel.ToUpper()) + "%" : "%";

            List<LOC_HIRCH_SEARCH_HEAD> crg = new List<LOC_HIRCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[7] = new OracleParameter("p_schannel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subChannel;
            (param[8] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[9] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[10] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_LOC_HIRCH_NEW", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<LOC_HIRCH_SEARCH_HEAD>(_dtResults, LOC_HIRCH_SEARCH_HEAD.Converter);
            }
            return crg;
        }
        //Dilshan 13-10-2017
        public List<LOC_HIRCH_SEARCH_HEAD> getLocHierarchyAll(string pgeNum, string pgeSize, string searchFld, string searchVal, string zone, string region, string area, string subChannel, string channel, string company, string type)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            zone = (!String.IsNullOrWhiteSpace(zone)) ? (zone.ToUpper()) + "%" : "%";
            region = (!String.IsNullOrWhiteSpace(region)) ? (region.ToUpper()) + "%" : "%";
            area = (!String.IsNullOrWhiteSpace(area)) ? (area.ToUpper()) + "%" : "%";
            subChannel = (!String.IsNullOrWhiteSpace(subChannel)) ? (subChannel.ToUpper()) + "%" : "%";
            channel = (!String.IsNullOrWhiteSpace(channel)) ? (channel.ToUpper()) + "%" : "%";

            List<LOC_HIRCH_SEARCH_HEAD> crg = new List<LOC_HIRCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = '%';
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[7] = new OracleParameter("p_schannel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subChannel;
            (param[8] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[9] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[10] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_LOC_HIRCH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<LOC_HIRCH_SEARCH_HEAD>(_dtResults, LOC_HIRCH_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        //Randima 02-08-2016
        public List<MAIN_CAT_SEARCH> getMainCategory(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_MAIN_CAT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 02-08-2016
        public List<ITM_STUS_SEARCH_HEAD> getItmOthStatus(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<ITM_STUS_SEARCH_HEAD> crg = new List<ITM_STUS_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "OTH";
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_ITM_STATUS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITM_STUS_SEARCH_HEAD>(_dtResults, ITM_STUS_SEARCH_HEAD.Converter);
            }
            return crg;
        }

        //Randima 03-08-2016
        public DataTable getAgeSlotForCom(string company)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_AGE_SLOT_COM", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Randima 03-08-2016
        public List<ITM_CUR_AGE_DET> getAsAtAgeItmDetails(string _cat1, string _brnd, DateTime _frmDt, DateTime _toDt, string _com, string _loc, string type, string model, string itemCd, string itmStustyp, string _cat2 = null, string _cat3 = null, string userid = null, string rpt = null)
        {
            List<ITM_CUR_AGE_DET> crg = new List<ITM_CUR_AGE_DET>();
            OracleParameter[] param = new OracleParameter[19];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDt;
            (param[2] = new OracleParameter("in_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDt;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[5] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[7] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[8] = new OracleParameter("in_cat4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[9] = new OracleParameter("in_cat5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[10] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[11] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[12] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[13] = new OracleParameter("in_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[14] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[15] = new OracleParameter("in_brnd_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[16] = new OracleParameter("in_agetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[17] = new OracleParameter("in_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itmStustyp;
            (param[18] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = rpt;

            //remove by nuwan and added new 
            //DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_comlocasat_age_scm", CommandType.StoredProcedure, false, param);
            DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_companyageasat", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITM_CUR_AGE_DET>(_dtResults, ITM_CUR_AGE_DET.ConverterSub);
            }
            return crg;
        }

        //Randima 03-08-2016 
        public List<BRND_NEW_STUS> getStatusForTyp(string _stus_typ)
        {
            List<BRND_NEW_STUS> crg = new List<BRND_NEW_STUS>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_stus_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus_typ;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_GET_ITM_STUS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BRND_NEW_STUS>(_dtResults, BRND_NEW_STUS.Converter);
            }
            return crg;
        }

        //Randima 04-08-2016
        public DataTable getlocByHirarchy(string _locHircCd, string _locHircDesc, string _company)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_mli_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _locHircCd;
            (param[1] = new OracleParameter("p_mli_val", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _locHircDesc;
            (param[2] = new OracleParameter("p_mli_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_LOC_BY_HIRCH", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Randima 05-08-2016
        public List<ITM_CUR_AGE_DET> getCurAgeItmDetails(string _cat1, string _brnd, string _com, string _loc, string type, string model, string itemCd)
        {
            try
            {
                List<ITM_CUR_AGE_DET> crg = new List<ITM_CUR_AGE_DET>();
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[15];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param[2] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
                (param[3] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[4] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[5] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[6] = new OracleParameter("in_cat4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[7] = new OracleParameter("in_cat5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[8] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
                (param[9] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
                (param[10] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
                (param[11] = new OracleParameter("in_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[12] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[13] = new OracleParameter("in_brnd_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[14] = new OracleParameter("in_agetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_comloccurr_age", CommandType.StoredProcedure, false, param);
                //_dtResults = QueryDataTable("tmpRepSer", "sp_get_comloccurr_age", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<ITM_CUR_AGE_DET>(_dtResults, ITM_CUR_AGE_DET.ConverterSub);
                }
                return crg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InventoryHeader> test()
        {
            List<InventoryHeader> crg = new List<InventoryHeader>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.test", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<InventoryHeader>(_dtResults, InventoryHeader.ConvertTotal);
            }
            return crg;
        }

        public List<SAR_FOR_HED> getTargetHedDetails(string sesCompany, string dtRange, string defby)
        {
            List<SAR_FOR_HED> crg = new List<SAR_FOR_HED>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sesCompany;
            (param[1] = new OracleParameter("p_pdcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dtRange;
            (param[2] = new OracleParameter("p_defcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tab", "pkg_dashboard.sp_get_sarvaldetails", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<SAR_FOR_HED>(_dtResults, SAR_FOR_HED.Converter);
            }
            return crg;
        }

        //Randima 11-08-2016
        public List<INVOICE_TYPE_SEARCH> getInvoiceTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }
            List<INVOICE_TYPE_SEARCH> crg = new List<INVOICE_TYPE_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_INVOICE_TYPES", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<INVOICE_TYPE_SEARCH>(_dtResults, INVOICE_TYPE_SEARCH.Converter);
            }
            return crg;
        }

        public List<SAR_FOR_DET> getFutureTargetData(string selCompany, string brandsdata, string allbrnd, string allbrndmngr, DateTime preYerdtStart, DateTime preYerdtEnd, string defby, string filterVal, string targetCode)
        {
            List<SAR_FOR_DET> sale = new List<SAR_FOR_DET>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selCompany;
            (param[1] = new OracleParameter("p_allbrnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = allbrnd;
            (param[2] = new OracleParameter("p_allbrndmngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = allbrndmngr;
            (param[3] = new OracleParameter("p_brands", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brandsdata;
            (param[4] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
            (param[5] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterVal;
            (param[6] = new OracleParameter("p_tgtcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = targetCode;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable data = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_TARGETFUTUREDATA", CommandType.StoredProcedure, false, param);
            if (data.Rows.Count > 0)
            {
                sale = DataTableExtensions.ToGenericList<SAR_FOR_DET>(data, SAR_FOR_DET.Converter);
            }
            return sale;
        }

        //Randima 15-08-2016
        public List<DELI_SALE> getFastMoveItem_SalesDetails(DateTime _fdate, DateTime _tDate, string invType, string _brnd, string _cat1, string _com, string _pc, string _freeIss, string qty)
        {
            List<DELI_SALE> crg = new List<DELI_SALE>();

            if (qty == "")
            {
                qty = null;
            }
            OracleParameter[] param = new OracleParameter[10];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tDate;
            (param[3] = new OracleParameter("in_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invType;
            (param[4] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[5] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[7] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[8] = new OracleParameter("in_freeiss", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _freeIss;
            (param[9] = new OracleParameter("in_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = qty;

            DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_deliveredsalesfst", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<DELI_SALE>(_dtResults, DELI_SALE.Converter);
            }
            return crg;
        }

        //Randima 15-08-2016
        public DataTable getPCByHirarchy(string _PCHircCd, string _PCHircDesc, string _company)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_mpi_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _PCHircCd;
            (param[1] = new OracleParameter("p_mpi_val", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _PCHircDesc;
            (param[2] = new OracleParameter("p_mpi_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_PC_BY_HIRCH", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Randima 18-08-2016
        public List<ITM_ASAT_BAL> getAsatBalance(string _brand, string _cat1, DateTime _asatDt, string _com, string _loc)
        {
            List<ITM_ASAT_BAL> crg = new List<ITM_ASAT_BAL>();

            OracleParameter[] param = new OracleParameter[14];
            param[0] = new OracleParameter("n_cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            (param[2] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[3] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[4] = new OracleParameter("in_itemstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[5] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[7] = new OracleParameter("in_itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[8] = new OracleParameter("in_withcost", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[9] = new OracleParameter("in_as_at_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _asatDt;
            (param[10] = new OracleParameter("in_with_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[11] = new OracleParameter("in_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[12] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[13] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;

            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.PROC_INVENTORY_BALANCE_ASAT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITM_ASAT_BAL>(_dtResults, ITM_ASAT_BAL.Converter);
            }
            return crg;
        }

        //Randima 19-08-2016
        public List<ITM_NEW_ARRIVAL> getNewArrivalsForMonth(string _year, string _month)
        {
            List<ITM_NEW_ARRIVAL> crg = new List<ITM_NEW_ARRIVAL>();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "A";
            (param[1] = new OracleParameter("p_year", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _year;
            (param[2] = new OracleParameter("p_month", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _month;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_NEW_ARRIVALS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITM_NEW_ARRIVAL>(_dtResults, ITM_NEW_ARRIVAL.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-30
        public List<ITEM_SEARCH> getItems(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }
            List<ITEM_SEARCH> crg = new List<ITEM_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_ITEMS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITEM_SEARCH>(_dtResults, ITEM_SEARCH.Converter);
            }
            return crg;
        }


        //Dilshan 2017-12-06
        public List<MST_TOWN_SEARCH> getTown(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_TOWN_SEARCH> crg = new List<MST_TOWN_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_TOWNS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_TOWN_SEARCH>(_dtResults, MST_TOWN_SEARCH.Converter);
            }
            return crg;
        }
        //Dilshan 2017-12-06
        public List<MST_PROMOTOR_SEARCH> getPromotor(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_PROMOTOR_SEARCH> crg = new List<MST_PROMOTOR_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_PROMOTOR", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_PROMOTOR_SEARCH>(_dtResults, MST_PROMOTOR_SEARCH.Converter);
            }
            return crg;
        }
        //Dilshan 2017-12-06
        public List<MST_ADMINT_SEARCH> getAdmint(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_ADMINT_SEARCH> crg = new List<MST_ADMINT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_ADMINTEAM", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_ADMINT_SEARCH>(_dtResults, MST_ADMINT_SEARCH.Converter);
            }
            return crg;
        }
        //Dilshan 2018/06/25**********************
        public List<MST_TEAML_SEARCH> getTeaml(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_TEAML_SEARCH> crg = new List<MST_TEAML_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_teamlead", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_TEAML_SEARCH>(_dtResults, MST_TEAML_SEARCH.Converter);
            }
            return crg;
        }

        public List<MST_MNGR_SEARCH> getMangr(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_MNGR_SEARCH> crg = new List<MST_MNGR_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_manager", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_MNGR_SEARCH>(_dtResults, MST_MNGR_SEARCH.Converter);
            }
            return crg;
        }
        public List<MST_ITMSTS_SEARCH> getItmsts(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_ITMSTS_SEARCH> crg = new List<MST_ITMSTS_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_itmstatus", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_ITMSTS_SEARCH>(_dtResults, MST_ITMSTS_SEARCH.Converter);
            }
            return crg;
        }
        public List<MST_LOYALTY_SEARCH> getLtltyp(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_LOYALTY_SEARCH> crg = new List<MST_LOYALTY_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_loyaltydetails", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_LOYALTY_SEARCH>(_dtResults, MST_LOYALTY_SEARCH.Converter1);
            }
            return crg;
        }
        public List<MST_LOYALTY_SEARCH> getLtlno(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_LOYALTY_SEARCH> crg = new List<MST_LOYALTY_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_loyaltynodetails", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_LOYALTY_SEARCH>(_dtResults, MST_LOYALTY_SEARCH.Converter);
            }
            return crg;
        }
        //***************************************
        //Dilshan 2018-02-17
        public List<MST_DIST_SEARCH> getDistrict(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_DIST_SEARCH> crg = new List<MST_DIST_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_districts", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_DIST_SEARCH>(_dtResults, MST_DIST_SEARCH.Converter2);
            }
            return crg;
        }

        //Dilshan 2018-02-17
        public List<MST_DIST_SEARCH> getProvince(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "";
            string desc = "";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        desc = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "";
                        desc = "";
                        break;

                }
            }
            List<MST_DIST_SEARCH> crg = new List<MST_DIST_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_search_provinces", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_DIST_SEARCH>(_dtResults, MST_DIST_SEARCH.Converter);
            }
            return crg;
        }

        //Dilshan 2017-12-06
        public List<BANK_SEARCH> getBank(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }
            List<BANK_SEARCH> crg = new List<BANK_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_GET_BANKS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BANK_SEARCH>(_dtResults, BANK_SEARCH.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-30
        public List<MAIN_CAT2_SEARCH> getCategory2(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT2_SEARCH> crg = new List<MAIN_CAT2_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CAT2", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT2_SEARCH>(_dtResults, MAIN_CAT2_SEARCH.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-30
        public List<MAIN_CAT3_SEARCH> getCategory3(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT3_SEARCH> crg = new List<MAIN_CAT3_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CAT3", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT3_SEARCH>(_dtResults, MAIN_CAT3_SEARCH.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-30
        public List<ITEM_BRAND_SEARCH> getItemBrands(string pgeNum, string pgeSize, string searchFld, string searchVal, string companies)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<ITEM_BRAND_SEARCH> crg = new List<ITEM_BRAND_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_ITEM_BRANDS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITEM_BRAND_SEARCH>(_dtResults, ITEM_BRAND_SEARCH.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-30
        public List<ITEM_MODEL_SEARCH> getItemModel(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<ITEM_MODEL_SEARCH> crg = new List<ITEM_MODEL_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_ITEM_MODEL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITEM_MODEL_SEARCH>(_dtResults, ITEM_MODEL_SEARCH.Converter);
            }
            return crg;
        }

        //Lakshika 2016-08-31
        public List<BM_SALE_DETAILS> getBMTSaleReportDetails(DateTime _frmDate, DateTime _toDate, string _saleType, string _itemCode,
                                             string _cat1, string _cat2, string _cat3, string _brand, string _itemModel, string _groupBy, string _com)
        {
            List<BM_SALE_DETAILS> crg = new List<BM_SALE_DETAILS>();

            OracleParameter[] param = new OracleParameter[11];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_frmDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDate;
            (param[2] = new OracleParameter("in_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
            (param[3] = new OracleParameter("in_saleType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _saleType;
            (param[4] = new OracleParameter("in_itemCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[5] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[7] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[8] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            (param[9] = new OracleParameter("in_itemModel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemModel;
            (param[10] = new OracleParameter("in_groupBy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _groupBy;

            //(param[6] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            //(param[7] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;

            DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.SP_GET_BM_SALE_DETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BM_SALE_DETAILS>(_dtResults, BM_SALE_DETAILS.Converter);
            }
            else
            {
                //OracleParameter[] param1 = new OracleParameter[22];
                //param1[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                //(param1[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = "01/Sep/2016";//_frmDate;
                //(param1[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = "21/Sep/2016";
                //(param1[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[4] = new OracleParameter("in_exec", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[5] = new OracleParameter("in_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[6] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";// _itemCode;
                //(param1[7] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";//_brand;
                //(param1[8] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";//_itemModel;
                //(param1[9] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";// _cat1;
                //(param1[10] = new OracleParameter("in_itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";//_cat2;
                //(param1[11] = new OracleParameter("in_itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";// _cat3;
                //(param1[12] = new OracleParameter("in_pcenter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[13] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[14] = new OracleParameter("in_stktype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[15] = new OracleParameter("in_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[16] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "ABC";
                //(param1[17] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "104";
                //(param1[18] = new OracleParameter("IN_PROMOTOR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[19] = new OracleParameter("in_freeIss", OracleDbType.Int32, 0, ParameterDirection.Input)).Value = 0;
                //(param1[20] = new OracleParameter("in_itemcat4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";
                //(param1[21] = new OracleParameter("in_itemcat5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = " ";

                //DataTable _dtResults1 = QueryDataTable("tmpRepSer", "pkg_dashboard.SP_GET_DELIVEREDSALES", CommandType.StoredProcedure, false, param1);


                //List<BM_SALE_DETAILS> crg1 = new List<BM_SALE_DETAILS>();


                OracleParameter[] param1 = new OracleParameter[13];
                param1[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param1[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDate;
                (param1[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param1[3] = new OracleParameter("in_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _saleType;
                (param1[4] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
                (param1[5] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
                (param1[6] = new OracleParameter("in_itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
                (param1[7] = new OracleParameter("in_itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
                (param1[8] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param1[9] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "104";
                (param1[10] = new OracleParameter("in_freeiss", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                (param1[11] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemModel;
                (param1[12] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
                DataTable _dtResults1 = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_deliveredsales_summary", CommandType.StoredProcedure, false, param1);

                DataTable dtTemp = new DataTable();
                DataRow redr;
                //Header Details
                dtTemp.Columns.Add("BMS_PC_CHNL", typeof(string));
                dtTemp.Columns.Add("NUMBER_OF_SALES", typeof(string));
                dtTemp.Columns.Add("SALE_VALUE", typeof(string));

                int i = 0;
                foreach (var dis in _dtResults1.Rows)
                {
                    redr = dtTemp.NewRow();

                    redr["BMS_PC_CHNL"] = _dtResults1.Rows[i]["PC_CODE"].ToString();
                    redr["NUMBER_OF_SALES"] = _dtResults1.Rows[i]["INV_NO"].ToString();
                    redr["SALE_VALUE"] = _dtResults1.Rows[i]["TOT_AMT"].ToString();

                    dtTemp.Rows.Add(redr);
                    i++;
                }

                if (dtTemp.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<BM_SALE_DETAILS>(dtTemp, BM_SALE_DETAILS.Converter);
                }

            }

            return crg;
        }

        //Lakshika 2016-09-01
        public List<BM_SALE_DETAILS> getSaleReportActiveDetails(DateTime _frmDate, DateTime _toDate, string _saleType, string _itemCode,
                                             string _cat1, string _cat2, string _cat3, string _brand, string _itemModel, string _groupBy, string _com)
        {
            List<BM_SALE_DETAILS> crg = new List<BM_SALE_DETAILS>();

            OracleParameter[] param = new OracleParameter[11];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_frmDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDate;
            (param[2] = new OracleParameter("in_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
            (param[3] = new OracleParameter("in_saleType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _saleType;
            (param[4] = new OracleParameter("in_itemCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[5] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[7] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[8] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            (param[9] = new OracleParameter("in_itemModel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemModel;
            (param[10] = new OracleParameter("in_groupBy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _groupBy;

            DataTable _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.SP_GET_ACTIVE_ACC_DETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<BM_SALE_DETAILS>(_dtResults, BM_SALE_DETAILS.ConverterSub);
            }

            else
            {
                //List<DELI_SALE> crg1 = new List<DELI_SALE>();

                //OracleParameter[] param1 = new OracleParameter[10];
                //param1[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                //(param1[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDate;
                //(param1[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                //(param1[3] = new OracleParameter("in_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _saleType;
                //(param1[4] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
                //(param1[5] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
                //(param1[6] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "ABL";
                //(param1[7] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "104";
                //(param1[8] = new OracleParameter("in_freeiss", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                //(param1[9] = new OracleParameter("in_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;

                //DataTable _dtResults1 = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_deliveredsales", CommandType.StoredProcedure, false, param1);
                //if (_dtResults1.Rows.Count > 0)
                //{
                //    crg1 = DataTableExtensions.ToGenericList<DELI_SALE>(_dtResults1, DELI_SALE.Converter);

                //}


                OracleParameter[] param1 = new OracleParameter[13];
                param1[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param1[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDate;
                (param1[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDate;
                (param1[3] = new OracleParameter("in_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _saleType;
                (param1[4] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
                (param1[5] = new OracleParameter("in_itemcat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
                (param1[6] = new OracleParameter("in_itemcat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
                (param1[7] = new OracleParameter("in_itemcat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
                (param1[8] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param1[9] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "104";
                (param1[10] = new OracleParameter("in_freeiss", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                (param1[11] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemModel;
                (param1[12] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;

                DataTable _dtResults1 = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_deliveredsales_summary", CommandType.StoredProcedure, false, param1);

                DataTable dtTemp = new DataTable();
                DataRow redr;
                //Header Details
                dtTemp.Columns.Add("BMS_PC_CHNL", typeof(string));
                dtTemp.Columns.Add("NUMBER_OF_SALES", typeof(string));
                dtTemp.Columns.Add("SALE_VALUE", typeof(string));

                int i = 0;
                foreach (var dis in _dtResults1.Rows)
                {
                    redr = dtTemp.NewRow();

                    redr["BMS_PC_CHNL"] = _dtResults1.Rows[i]["PC_CODE"].ToString();
                    redr["NUMBER_OF_SALES"] = _dtResults1.Rows[i]["INV_NO"].ToString();
                    redr["SALE_VALUE"] = _dtResults1.Rows[i]["TOT_AMT"].ToString();

                    dtTemp.Rows.Add(redr);
                    i++;
                }

                if (dtTemp.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<BM_SALE_DETAILS>(dtTemp, BM_SALE_DETAILS.Converter);
                }

            }

            return crg;
        }

        //Randima 2016-09-05
        public List<MAIN_CAT3_SEARCH> getCategory4(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT3_SEARCH> crg = new List<MAIN_CAT3_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CAT4", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT3_SEARCH>(_dtResults, MAIN_CAT3_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-05
        public List<MAIN_CAT3_SEARCH> getCategory5(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT3_SEARCH> crg = new List<MAIN_CAT3_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CAT5", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT3_SEARCH>(_dtResults, MAIN_CAT3_SEARCH.Converter);
            }
            return crg;
        }


        //Randima 2016-09-05
        public List<LOC_HIRCH_SEARCH_HEAD> getAllCompanies(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<LOC_HIRCH_SEARCH_HEAD> crg = new List<LOC_HIRCH_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_ALL_COM", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<LOC_HIRCH_SEARCH_HEAD>(_dtResults, LOC_HIRCH_SEARCH_HEAD.Converter);
            }
            return crg;
        }


        //Randima 2016-09-06
        public List<CIRCULAR_NO_SEARCH> getCircualrNo(string pgeNum, string pgeSize, string cir_no, DateTime _frmDt, DateTime _toDt)
        {
            string p_cir_no = (!string.IsNullOrEmpty(cir_no)) ? "%" + cir_no + "%" : "%";

            List<CIRCULAR_NO_SEARCH> crg = new List<CIRCULAR_NO_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_cir_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_cir_no;
            (param[3] = new OracleParameter("p_frm_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDt;
            (param[4] = new OracleParameter("p_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDt;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CIRCULAR", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<CIRCULAR_NO_SEARCH>(_dtResults, CIRCULAR_NO_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<MAIN_CAT_SEARCH> getSchemeType(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_SCHM_TP", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }


        //Randima 2016-09-06
        public List<MAIN_CAT_SEARCH> getSchemeCode(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_SCHM_CD", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }

        public List<MAIN_CAT_SEARCH> getSchemeTerm(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_SCHM_TERM", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }
        public List<MAIN_CAT_SEARCH> getPtypeCode(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_PTYPE_CD", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<MAIN_CAT_SEARCH> getAllPriceBooks(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_PRICE_BK", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<MAIN_CAT_SEARCH> getAllPriceLevels(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_PRICE_LVL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<CUSTOMER_SEARCH> getCustomers(string pgeNum, string pgeSize, string code, string name, string add, string company)
        {
            string _code = (!string.IsNullOrEmpty(code)) ? "%" + code + "%" : "%";
            string _name = (!string.IsNullOrEmpty(name)) ? "%" + name + "%" : "%";
            string _add = (!string.IsNullOrEmpty(add)) ? "%" + add + "%" : "%";


            List<CUSTOMER_SEARCH> crg = new List<CUSTOMER_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[3] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _name;
            (param[4] = new OracleParameter("p_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _add;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_CUSTOMER", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<CUSTOMER_SEARCH>(_dtResults, CUSTOMER_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<EXECUTIVE_SEARCH> getAllExecutives(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            //string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : null;

            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string epf = "%";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "EPF":
                        epf = search.ToUpper();
                        break;
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Name":
                        desc = search.ToUpper();
                        break;
                    default:
                        epf = "%";
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<EXECUTIVE_SEARCH> crg = new List<EXECUTIVE_SEARCH>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_epf", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = epf;
            (param[3] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[4] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_EXECUTIVE", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<EXECUTIVE_SEARCH>(_dtResults, EXECUTIVE_SEARCH.Converter);
            }
            return crg;
        }

        //Randima 2016-09-06
        public List<MAIN_CAT_SEARCH> getInvoiceSubTypes(string pgeNum, string pgeSize, string searchFld, string searchVal, string mainTyp, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            string code = "%";
            string desc = "%";


            if (!string.IsNullOrEmpty(search))
            {
                switch (searchFld)
                {
                    case "Code":
                        code = search.ToUpper();
                        break;
                    case "Description":
                        desc = search.ToUpper();
                        break;
                    default:
                        code = "%";
                        desc = "%";
                        break;

                }
            }

            List<MAIN_CAT_SEARCH> crg = new List<MAIN_CAT_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            //(param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[4] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mainTyp;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_INVOICE_SUB_TP", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT_SEARCH>(_dtResults, MAIN_CAT_SEARCH.Converter);
            }
            return crg;
        }
        //Subodana 2016-10-25
        public List<Sim_Pc> GetPcInfoData(string _code, string _val)
        {
            List<Sim_Pc> _Ref = new List<Sim_Pc>();
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("P_VALUE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _val;
            param[2] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tbl = QueryDataTable("tbl", "GetPcInfoData", CommandType.StoredProcedure, false, param);
            if (_tbl.Rows.Count > 0)
            {
                _Ref = DataTableExtensions.ToGenericList<Sim_Pc>(_tbl, Sim_Pc.Converter);
            }
            return _Ref;

        }
        //SUBODANA 2017-01-28
        public List<Forcstdata> GetForecatData(string com, DateTime date, string item, string codes, string cat1, string cat2, string model, string brand)
        {
            List<Forcstdata> oResult = null;
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
            (param[2] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[3] = new OracleParameter("P_CODES", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = codes;
            (param[4] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat1;
            (param[5] = new OracleParameter("P_CAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
            (param[6] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[7] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "sp_get_forcst_data", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<Forcstdata>(dtTemp, Forcstdata.Converter);
            }
            return oResult;
        }
        //SUBODANA 2017-02-02
        public List<DeliveryItemDetails> GetDiliverdItemList(string com, DateTime Fdate, DateTime Todate, string chnl, string schnl, string area, string zone, string region, string item, string cat1, string cat2, string model, string brand)
        {
            List<DeliveryItemDetails> oResult = null;
            OracleParameter[] param = new OracleParameter[14];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[2] = new OracleParameter("P_SCHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            (param[3] = new OracleParameter("P_AREA", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[4] = new OracleParameter("P_ZONE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            (param[5] = new OracleParameter("P_REGION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = Fdate;
            (param[7] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = Todate;
            (param[8] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[9] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat1;
            (param[10] = new OracleParameter("P_CAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
            (param[11] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[12] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            param[13] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "sp_get_delivert_items", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<DeliveryItemDetails>(dtTemp, DeliveryItemDetails.Converter);
            }
            return oResult;
        }
        public List<DeliveryItemDetails> GetDiliverdItemListcurrent(string com, DateTime Fdate, DateTime Todate, string chnl, string schnl, string area, string zone, string region, string item, string cat1, string cat2, string model, string brand)
        {
            List<DeliveryItemDetails> oResult = null;
            OracleParameter[] param = new OracleParameter[14];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[2] = new OracleParameter("P_SCHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            (param[3] = new OracleParameter("P_AREA", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[4] = new OracleParameter("P_ZONE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            (param[5] = new OracleParameter("P_REGION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = Fdate;
            (param[7] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = Todate;
            (param[8] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[9] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat1;
            (param[10] = new OracleParameter("P_CAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
            (param[11] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[12] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            param[13] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "sp_get_delivert_itemscurrent", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<DeliveryItemDetails>(dtTemp, DeliveryItemDetails.Converter);
            }
            return oResult;
        }
        public DataTable GetWekklydays(string company, int month, int week)
        {
            OracleParameter[] param = new OracleParameter[4];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_MONTH", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = month;
            (param[2] = new OracleParameter("P_WEEK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = week;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_MONTHDAYS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Nadeeka write copy by Lakshan 14 Feb 2017
        public MasterItem GetItemMaster(string _item)
        {
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
        //Lakshan 14 Feb 2017
        public List<RptWarehousStockBalance> GetItemDataForDailyWareHouseBalnceOld(MasterItem _mstItm)
        {
            List<RptWarehousStockBalance> _list = new List<RptWarehousStockBalance>();
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_inl_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Tmp_com;
            (param[1] = new OracleParameter("p_mi_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_model;
            (param[2] = new OracleParameter("p_mi_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_brand;
            (param[3] = new OracleParameter("p_mi_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cate_1;
            (param[4] = new OracleParameter("p_mi_cate_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cate_2;
            (param[5] = new OracleParameter("p_mi_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cd;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_get_itms_bt_daily_wh_stock", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<RptWarehousStockBalance>(_dtResults, RptWarehousStockBalance.Converter);
            }
            if (_list != null)
            {
                if (_list.Count > 0)
                {
                    _list = _list.OrderByDescending(c => c.Is_df_loc).ToList();
                }
            }
            return _list;
        }

        public List<ITEM_MODEL_SEARCH> getInventryAgeItems(string company, string brndMngr, string brnd, string cate1, string pagenum, string pagesize, string field, string search)
        {
            try
            {
                List<ITEM_MODEL_SEARCH> oResult = new List<ITEM_MODEL_SEARCH>(); ;


                switch (field)
                {
                    case "Model":
                        field = "MM_CD";
                        break;
                    case "Description":
                        field = "MM_DESC";
                        break;
                    case "Brand":
                        field = "MM_BRAND";
                        break;
                    case "Category 1":
                        field = "MM_CAT1";
                        break;
                    default:
                        field = "";
                        break;

                }
                OracleParameter[] param = new OracleParameter[9];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_brndmngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brndMngr;
                (param[2] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brnd;
                (param[3] = new OracleParameter("p_cate1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate1;
                (param[4] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pagenum);
                (param[5] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pagesize);
                (param[6] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = field;
                (param[7] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable dtTemp = QueryDataTable("data", "pkg_dashboard.SP_SEARCH_ITEMMODELS", CommandType.StoredProcedure, false, param);
                if (dtTemp.Rows.Count > 0)
                {
                    oResult = DataTableExtensions.ToGenericList<ITEM_MODEL_SEARCH>(dtTemp, ITEM_MODEL_SEARCH.ConverterItem);
                }
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //NUWAN 17-02-2017
        public DataTable getAgeSlotForComALL(string company)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_SEARCH_AGE_SLOT_COMNU", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<ITEM_SEARCH> getInventryAgeSrchItm(string company, string brndMngr, string brnd, string cate1, string model, string pagenum, string pagesize, string field, string search)
        {
            try
            {
                List<ITEM_SEARCH> oResult = new List<ITEM_SEARCH>();
                switch (field)
                {
                    case "Code":
                        field = "MI_CD";
                        break;
                    case "Description":
                        field = "MI_SHORTDESC";
                        break;
                    default:
                        field = "";
                        break;
                }

                brnd = (brnd != null && brnd != "") ? (brnd + "%") : null;
                cate1 = (cate1 != null && cate1 != "") ? (cate1 + "%") : null;
                model = (model != null && model != "") ? (model + "%") : null;
                search = (search != null && search != "") ? (search + "%") : null;

                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_brndmngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brndMngr;
                (param[2] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brnd;
                (param[3] = new OracleParameter("p_cate1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate1;
                (param[4] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
                (param[5] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pagenum);
                (param[6] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pagesize);
                (param[7] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = field;
                (param[8] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable dtTemp = QueryDataTable("data", "pkg_dashboard.SP_SEARCH_ITEMM", CommandType.StoredProcedure, false, param);
                if (dtTemp.Rows.Count > 0)
                {
                    oResult = DataTableExtensions.ToGenericList<ITEM_SEARCH>(dtTemp, ITEM_SEARCH.ConverterItm);
                }
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Lakshan 23 Feb 2017
        public List<RptWarehousStockBalance> GetItemDataForDailyWareHouseBalnce(MasterItem _mstItm, DateTime _dtCurr)
        {
            List<RptWarehousStockBalance> _list = new List<RptWarehousStockBalance>();
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_inl_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Tmp_com;
            (param[1] = new OracleParameter("p_mi_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_model;
            (param[2] = new OracleParameter("p_mi_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_brand;
            (param[3] = new OracleParameter("p_mi_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cate_1;
            (param[4] = new OracleParameter("p_mi_cate_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cate_2;
            (param[5] = new OracleParameter("p_mi_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mstItm.Mi_cd;
            (param[6] = new OracleParameter("p_cur_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtCurr;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_bt_dw_get_itm_data", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<RptWarehousStockBalance>(_dtResults, RptWarehousStockBalance.Converter);
            }
            if (_list != null)
            {
                if (_list.Count > 0)
                {
                    _list = _list.OrderByDescending(c => c.Is_df_loc).ToList();
                }
            }
            return _list;
        }
        //Lakshan 2017 FEB 02
        public decimal GetGitBalanceForItem(RptWarehousStockBalance _obj)
        {
            decimal _gitVal = 0, _tmp = 0;
            OracleParameter[] param = new OracleParameter[4];
            //  (param[0] = new OracleParameter("p_Tus_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _seq;
            (param[0] = new OracleParameter("p_ml_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Loc_tp;
            (param[1] = new OracleParameter("p_ith_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Mi_com;
            (param[2] = new OracleParameter("p_itb_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Mi_cd;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dt = QueryDataTable("temp_pick_ser", "sp_bt_dw_get_itm_git", CommandType.StoredProcedure, false, param);
            if (_dt != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    _gitVal = decimal.TryParse(_dt.Rows[0][0].ToString(), out _tmp) ? Convert.ToDecimal(_dt.Rows[0][0].ToString()) : 0;
                }
            }
            return _gitVal;
        }

        public List<MST_CHNL_SEARH_HEAD> getAllChannel(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            List<MST_CHNL_SEARH_HEAD> oResult = new List<MST_CHNL_SEARH_HEAD>(); ;


            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pgeNum);
            (param[2] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pgeSize);
            (param[3] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[4] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "pkg_dashboard.SP_SEARCH_ALLCHANEL", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<MST_CHNL_SEARH_HEAD>(dtTemp, MST_CHNL_SEARH_HEAD.Converter);
            }
            return oResult;
        }
        public List<CHANL_WISE_SALES> getChannelWiseSales(string selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[23];
                param[0] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[4] = new OracleParameter("IN_EXEC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[5] = new OracleParameter("IN_DOCTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[6] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[7] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[8] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[9] = new OracleParameter("IN_ITEMCAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[10] = new OracleParameter("IN_ITEMCAT2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[11] = new OracleParameter("IN_ITEMCAT3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[12] = new OracleParameter("IN_PCENTER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[13] = new OracleParameter("IN_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[14] = new OracleParameter("IN_STKTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[15] = new OracleParameter("IN_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[16] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[17] = new OracleParameter("IN_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[18] = new OracleParameter("IN_PROMOTOR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[19] = new OracleParameter("in_freeIss", OracleDbType.Int16, null, ParameterDirection.Input)).Value = 0;
                (param[20] = new OracleParameter("IN_CURRENCYTYPE", OracleDbType.Int16, null, ParameterDirection.Input)).Value = 0;
                (param[21] = new OracleParameter("IN_SALETYPE", OracleDbType.Int16, null, ParameterDirection.Input)).Value = 2;
                (param[22] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                DataTable _dtResults = new DataTable(); ;
                if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "pkg_dashboard.PROC_TOT_SALES_PCWISE", CommandType.StoredProcedure, false, param);
                }
                else
                {
                    _dtResults = QueryDataTable("tbl", "pkg_dashboard.PROC_TOT_SALES", CommandType.StoredProcedure, false, param);
                }

                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.Converter);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CHANL_WISE_SALES> getChnlDeliverdSale(string selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[4] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[5] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[6] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[7] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[8] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                DataTable _dtResults = new DataTable();
                if (filterby == "CHNLWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_CHNLDELEVERYSALES", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterChnlDelivery);
                    }
                }
                else if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_PCDELEVERYSALES", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterPcDelivery);
                    }
                }



                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getDeliverdSaleDetails(string selectedcompany, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[4] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[5] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[6] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[7] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[8] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                DataTable _dtResults = new DataTable(); ;
                if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "pkg_dashboard.SP_GET_DELIVEREDSALESPC", CommandType.StoredProcedure, false, param);
                }
                else
                {
                    _dtResults = QueryDataTable("tbl", "pkg_dashboard.SP_GET_DELIVEREDSALESCUR", CommandType.StoredProcedure, false, param);

                }



                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.Converter);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Lakshan 23 Feb 2017
        public decimal GetItemDailyAVGDispatch(string _com, string _itm, string _cat, DateTime _dtFrom, DateTime _dtTo)
        {
            decimal _tmp = 0, _dec = 0;
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_ith_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_itb_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_ml_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[3] = new OracleParameter("p_dt_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            (param[4] = new OracleParameter("p_dt_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_bt_dw_get_itm_disp_avg", CommandType.StoredProcedure, false, param);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    _dec = decimal.TryParse(_dtResults.Rows[0][0].ToString(), out _tmp) ? Convert.ToDecimal(_dtResults.Rows[0][0].ToString()) : 0;
                }
            }
            return _dec;
        }
        //SUBODANA 2017-03-06
        public List<RatioData> GetRatioData(string com, DateTime fdate, DateTime tdate, string item, string cat1, string model, string brand, string doctype, string cattype, string itemstatus)
        {
            List<RatioData> oResult = null;
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdate;
            (param[3] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[4] = new OracleParameter("P_CAT1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat1;
            (param[5] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[6] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            (param[7] = new OracleParameter("P_DOCTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doctype;
            (param[8] = new OracleParameter("P_CATTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cattype;
            (param[9] = new OracleParameter("P_ITEMSTATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemstatus;
            param[10] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "sp_get_ratio_rep_data", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<RatioData>(dtTemp, RatioData.Converter);
            }
            return oResult;
        }
        public List<SerJobDesc> GetSerJobDesc(string ItemCode, DateTime fdate, DateTime tdate)
        {
            List<SerJobDesc> oResult = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ItemCode;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdate;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dtTemp = QueryDataTable("data", "SP_GET_JOBDESC", CommandType.StoredProcedure, false, param);
            if (dtTemp.Rows.Count > 0)
            {
                oResult = DataTableExtensions.ToGenericList<SerJobDesc>(dtTemp, SerJobDesc.Converter);
            }
            return oResult;
        }
        //Lakshan 23 Feb 2017
        public decimal GetItemExpectedArrivalDPS(string _com, string _itm, DateTime _dtFrom, DateTime _dtTo)
        {
            decimal _tmp = 0, _dec = 0;
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_io_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_ioi_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            (param[3] = new OracleParameter("p_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_bt_dw_get_exp_arr_dps", CommandType.StoredProcedure, false, param);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    _dec = decimal.TryParse(_dtResults.Rows[0][0].ToString(), out _tmp) ? Convert.ToDecimal(_dtResults.Rows[0][0].ToString()) : 0;
                }
            }
            return _dec;
        }
        //Lakshan 23 Feb 2017
        public decimal GetItemExpectedArrivalDFS(string _com, string _itm, DateTime _dtFrom, DateTime _dtTo)
        {
            decimal _tmp = 0, _dec = 0;
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_ib_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_ibi_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            (param[3] = new OracleParameter("p_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_bt_dw_get_exp_arr_dfs", CommandType.StoredProcedure, false, param);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    _dec = decimal.TryParse(_dtResults.Rows[0][0].ToString(), out _tmp) ? Convert.ToDecimal(_dtResults.Rows[0][0].ToString()) : 0;
                }
            }
            return _dec;
        }

        public List<ITM_CUR_AGE_DET> getAsAtAgeItmDetailsCurrent(string _cat1, string _brnd, DateTime _frmDt, DateTime _toDt, string _com, string loc, string type, string model, string itemCd, List<BRND_NEW_STUS> statusLst, string itmStustyp, string _cat2 = null, string _cat3 = null, string userid = null, string report = null)
        {
            List<ITM_CUR_AGE_DET> crg = new List<ITM_CUR_AGE_DET>();
            OracleParameter[] param = new OracleParameter[19];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _frmDt.Date;
            (param[2] = new OracleParameter("in_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _toDt.Date;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[5] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[6] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat2;
            (param[7] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat3;
            (param[8] = new OracleParameter("in_cat4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[9] = new OracleParameter("in_cat5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[10] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[11] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[12] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[13] = new OracleParameter("in_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[14] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[15] = new OracleParameter("in_brnd_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
            (param[16] = new OracleParameter("in_agetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[17] = new OracleParameter("in_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itmStustyp;
            (param[18] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;

            DataTable _dtResults = QueryDataTable("tmpRepSer", "sp_get_comlocasat_age_scmbk", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<ITM_CUR_AGE_DET>(_dtResults, ITM_CUR_AGE_DET.ConverterSub);
            }
            return crg;
        }
        public List<ITM_CUR_AGE_DET> getCurAgeItmDetailsmullocs(string _cat1, string _brnd, string _com, string _loc, string type, string model, string itemCd, string statusLst, string cat2 = null, string cat3 = null, string userid = null, string report = null)
        {
            try
            {
                List<ITM_CUR_AGE_DET> crg = new List<ITM_CUR_AGE_DET>();
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[17];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
                (param[2] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
                (param[3] = new OracleParameter("in_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
                (param[4] = new OracleParameter("in_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
                (param[5] = new OracleParameter("in_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat3;
                (param[6] = new OracleParameter("in_cat4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[7] = new OracleParameter("in_cat5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[8] = new OracleParameter("in_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
                (param[9] = new OracleParameter("in_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
                (param[10] = new OracleParameter("in_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
                (param[11] = new OracleParameter("in_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[12] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[13] = new OracleParameter("in_brnd_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = null;
                (param[14] = new OracleParameter("in_agetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[15] = new OracleParameter("in_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = statusLst;
                (param[16] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;


                _dtResults = QueryDataTable("tmpRepSer", "pkg_dashboard.sp_get_comloccurr_agemulloc", CommandType.StoredProcedure, false, param);
                //_dtResults = QueryDataTable("tmpRepSer", "sp_get_comloccurr_age", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<ITM_CUR_AGE_DET>(_dtResults, ITM_CUR_AGE_DET.ConverterSub);
                }
                return crg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get compare sales detail by dulanga  
        public DataTable GetCompareSales(DateTime fdate, DateTime todate, DateTime pfdate, DateTime ptodate,
            DateTime pmfdate, DateTime pmtodate, string com, string chanel, string pc)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[2] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = pfdate;
            (param[3] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = ptodate;
            (param[4] = new OracleParameter("p_pymnthstdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = pmfdate;
            (param[5] = new OracleParameter("p_pymnthenddt", OracleDbType.Date, null, ParameterDirection.Input)).Value = pmtodate;
            (param[6] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[7] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chanel;
            (param[8] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tblcrg", "SP_GET_DELSALESCOMPPY", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //get pc inquery sale by dulanga
        public DataTable GetInquaryPcSales(string p_pc, string p_cat, string p_com, DateTime p_stdt, DateTime p_enddt, string p_all)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[1] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_cat;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[3] = new OracleParameter("p_stdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_stdt;
            (param[4] = new OracleParameter("p_enddt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_enddt;
            (param[5] = new OracleParameter("p_all", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_all;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_GET_PCCATSALESAMOUNT", CommandType.StoredProcedure, false, param);

            return _dtResults;


        }



        public DataTable GetMobBMTSales(string p_com, string p_code, string p_defby, string p_start, string p_end)
        {  // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_code;
            (param[2] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_defby;
            (param[3] = new OracleParameter("p_start", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_start;
            (param[4] = new OracleParameter("p_end", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_end;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_BMT_SALE", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetMobBMTTargert(string p_pc, string p_code, string p_start, string p_end)
        {   // // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[1] = new OracleParameter("p_start", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_start;
            (param[2] = new OracleParameter("p_end", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_end;
            (param[3] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_code;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_BMT_TARGERT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        /// <summary>
        /// get Invoice sales sum
        /// </summary>
        /// <param name="p_com"></param>
        /// <param name="p_pc"></param>
        /// <param name="p_start"></param>
        /// <param name="p_end"></param>
        /// <param name="p_direct"></param>
        /// <returns></returns>
        public DataTable GetMobSales(string p_com, string p_pc, string p_start, string p_end, string p_direct, string type)
        {   // // dulanga 2017-8-02

            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[2] = new OracleParameter("p_start", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(p_start).Date;
            (param[3] = new OracleParameter("p_end", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(p_end).Date;
            (param[4] = new OracleParameter("p_direct", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_direct;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            if (type.Equals("I"))
            {
                _dtResults = QueryDataTable("tblRcc", "SP_MOB_SUM_INVOICE_SALES", CommandType.StoredProcedure, false, param);
            }
            else
            {
                _dtResults = QueryDataTable("tblRcc", "SP_MOB_SUM_DELIVER_SALES", CommandType.StoredProcedure, false, param);

            }
            return _dtResults;
        }


        //get Targert Period Mob 
        public DataTable GetMobBMTTargertPeriod(string p_type, string p_code)
        {   // // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_type;
            (param[1] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_code;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "SP_MOB_BMT_TARGERT_PERIOD", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        //get Specification by model by dulanga 
        public DataTable GetSpecificationByModel(string model)
        {   // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_specificationByModel", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetVideoByModel(string model)
        {    // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_videoByModel", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetColorByModel(string model)
        {   // dulanga 2017-6-23
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRcc", "sp_colorByModel", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }



        public DataTable GetAssetforMonth(string item, string seq, string status, bool isin)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seq;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            param[3] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            if (isin == true)
            {
                _dtResults = QueryDataTable("tblcrg", "SP_ASAT_NEW1", CommandType.StoredProcedure, false, param);
            }
            else
            {
                _dtResults = QueryDataTable("tblcrg", "SP_ASAT_NEWNOTIN", CommandType.StoredProcedure, false, param);
            }

            return _dtResults;
        }
        public DataTable GetAssetforAfterMonth(string company, string item, string status, string type, DateTime maxdate, DateTime fromdate, bool isin)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[3] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[4] = new OracleParameter("P_MAXDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = maxdate;
            (param[5] = new OracleParameter("P_FROMDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            param[6] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            if (isin == true)
            {
                _dtResults = QueryDataTable("tblcrg", "SP_ASAT_NEW2", CommandType.StoredProcedure, false, param);
            }
            else
            {
                _dtResults = QueryDataTable("tblcrg", "SP_ASAT_NEWNOTIN2", CommandType.StoredProcedure, false, param);
            }

            return _dtResults;
        }
        public DataTable GetInOutQty(string company, string item, string status, DateTime maxdate, DateTime todate, int dir)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[2] = new OracleParameter("P_DERECT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = dir;
            (param[3] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = maxdate;
            (param[4] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[5] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            param[6] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GETINOUTQTY", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable GetInOutQtyNot(string company, string item, string status, DateTime maxdate, DateTime todate, int dir)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[2] = new OracleParameter("P_DERECT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = dir;
            (param[3] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = maxdate;
            (param[4] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[5] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            param[6] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GETINOUTQTYNOT", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getPCByHirarchy(string company, string Channel, string Subchnl, string Region, string Zone, string pc)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[2] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[3] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Region;
                (param[4] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Zone;
                (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_PCBYHIARACHY", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CUSTOMER_SALES> getCustomerInvDetails(DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string company,
            string pclst, string MainCat, string Brand, string txtModel, string txtItem, string filterby, string cat2,
            string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype,
            string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype,
            string Channel, string Subchnl, string Area, string Region, string Zone, string pc, string user, string dist, string prov)
        {
            try
            {
                List<CUSTOMER_SALES> finres = new List<CUSTOMER_SALES>();
                OracleParameter[] param = new OracleParameter[32];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesFrom.ToString("dd/MMM/yyyy")).Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesTo.ToString("dd/MMM/yyyy")).Date;
                (param[3] = new OracleParameter("p_chkamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = CheckAmount;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[6] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[7] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Area;
                (param[8] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[9] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[10] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[11] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[12] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[13] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[14] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[15] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MainCat;
                (param[16] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
                (param[17] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat3;
                (param[18] = new OracleParameter("p_reptype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;
                (param[19] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                (param[20] = new OracleParameter("p_numberofvisit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = visit;
                (param[21] = new OracleParameter("p_age", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = age;
                (param[22] = new OracleParameter("p_salary", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = salary;
                (param[23] = new OracleParameter("p_custown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                (param[24] = new OracleParameter("p_purctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                (param[25] = new OracleParameter("p_saletype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                (param[26] = new OracleParameter("p_schematype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                (param[27] = new OracleParameter("p_schemacd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                (param[28] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;
                (param[29] = new OracleParameter("p_crdcdbnk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                (param[30] = new OracleParameter("p_dist", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dist;
                (param[31] = new OracleParameter("p_prov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prov;


                //(param[9] = new OracleParameter("p_itemcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                //(param[10] = new OracleParameter("p_filterby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;

                //(param[13] = new OracleParameter("p_visit", OracleDbType.Int32, null, ParameterDirection.Input)).Value = visit;
                //(param[14] = new OracleParameter("p_age", OracleDbType.Int32, null, ParameterDirection.Input)).Value = age;
                //(param[16] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                //(param[17] = new OracleParameter("p_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                //(param[18] = new OracleParameter("P_schemetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                //(param[19] = new OracleParameter("P_schemecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                //(param[20] = new OracleParameter("p_ctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                //(param[21] = new OracleParameter("p_ptown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                //(param[22] = new OracleParameter("p_bankcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                //(param[23] = new OracleParameter("p_withserial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Withserial;
                //(param[24] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;

                DataTable _dtResults = QueryDataTable("tblcrg", "SP_GET_CUSTOMERSALES_NEW1", CommandType.StoredProcedure, false, param);
                //DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_CUSTOMERSALES_NEW", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    //if (filterby == "item")
                    //{
                    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.ConverterAll);
                    //}
                    //else
                    //{
                    //    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.ConverterSub);
                    //}
                }

                return finres;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Isuru 2017/05/12
        public List<Ex_Rev_Data> getexchangereversaldetails(string selectedcompany, string Channel, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string Category, string type, string pc, string itemcode)
        {
            try
            {
                List<Ex_Rev_Data> sales = new List<Ex_Rev_Data>();
                OracleParameter[] param = new OracleParameter[10];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_selectedcompany", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[2] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[3] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[4] = new OracleParameter("p_txtModel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[5] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate.Date;
                (param[6] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate.Date;
                (param[7] = new OracleParameter("p_category", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[8] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[9] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;

                DataTable _dtResults = new DataTable();

                _dtResults = QueryDataTable("tbl", "SP_GET_EX_REV_DET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<Ex_Rev_Data>(_dtResults, Ex_Rev_Data.Converter);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<BarChartData> getPortAgentDetails(DateTime frmdt, DateTime toDt, string company)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartData> lst = new List<BarChartData>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_BLPORTDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                lst = DataTableExtensions.ToGenericList<BarChartData>(_dtResults, BarChartData.Converter);
            }
            return lst;
        }

        public DataTable getAllRelatedPorts(DateTime frmdt, DateTime toDt, string company)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_BLPORTS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getAllRelatedAgents(DateTime frmdt, DateTime toDt, string company)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_BLAGENTS", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public List<BarChartData> getFromPortData(DateTime fromdate, DateTime todate, string comlst, string port = null, string agent = null)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartData> lst = new List<BarChartData>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = port;
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agent;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_FILTERBLPORTDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                lst = DataTableExtensions.ToGenericList<BarChartData>(_dtResults, BarChartData.Converter);
            }
            return lst;
        }

        public List<BarChartDataPort> getPortTotal(DateTime frmdt, DateTime toDt, string comlst)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartDataPort> lst = new List<BarChartDataPort>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
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

        public List<BarChartDataAgent> getAgentTotal(DateTime frmdt, DateTime toDt, string comlst)
        {
            DataTable _dtResults = new DataTable();
            List<BarChartDataAgent> lst = new List<BarChartDataAgent>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
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
        public DataTable getShipmentContainers(DateTime frmdt, DateTime toDt, string comlst, string port, string agent)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = port;
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agent;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_CONTAINERSCOUNT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable getShipmentContainersByAgent(DateTime frmdt, DateTime toDt, string comlst, string port, string agent)
        {
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
            (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
            (param[3] = new OracleParameter("p_port", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = port;
            (param[4] = new OracleParameter("p_agent", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agent;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tblcrg", "SP_GET_CONTAINERSBYAGENT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<CHANL_WISE_SALES> getChnWiseDeliverySalesWithPre(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, DateTime preFrmDt, DateTime preToDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("p_prefDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = preFrmDt.Date;
                (param[4] = new OracleParameter("p_ftdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = preToDt.Date;
                (param[5] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[6] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[7] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[8] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[9] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[10] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[11] = new OracleParameter("p_curyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = frmDt.Year;
                (param[12] = new OracleParameter("p_preyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = preFrmDt.Year;
                DataTable _dtResults = new DataTable();
                if (filterby == "CHNLWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_CHNLDELEVERYSALESWTHPRE", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterChnlDeliveryPre);
                    }
                }
                else if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_PCDELEVERYSALESWTHPRE", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterPcDeliveryPre);
                    }
                }


                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getDelSalesWithCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[4] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[5] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[6] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[7] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[8] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                DataTable _dtResults = new DataTable();
                if (filterby == "CHNLWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_CATCHNLDELSALES", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterCatChnlDel);
                    }
                }
                else if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_PCCATDELSALES", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterCatPcDel);
                    }
                }


                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getDelSalesWithCateWithPre(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, DateTime fromdate, DateTime todate, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("p_prefdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate.Date;
                (param[4] = new OracleParameter("p_pretdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate.Date;
                (param[5] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[6] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[7] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[8] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[9] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[10] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[11] = new OracleParameter("p_curyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = frmDt.Year;
                (param[12] = new OracleParameter("p_preyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = fromdate.Year;
                DataTable _dtResults = new DataTable();
                if (filterby == "CHNLWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_CATCHNLDELSALESPY", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterCatChnlDelpy);
                    }
                }
                else if (filterby == "PCWISE")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_PCCATDELSALESPY", CommandType.StoredProcedure, false, param);
                    if (_dtResults.Rows.Count > 0)
                    {
                        sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterCatPcDelPy);
                    }
                }


                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getSpecialCriSales(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[4] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[5] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[6] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[7] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[8] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                DataTable _dtResults = new DataTable();

                _dtResults = QueryDataTable("tbl", "SP_GET_SPCRITERIADELSALE", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterSp);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getSpecialCriSalesCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[4] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[5] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[6] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[7] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[8] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                DataTable _dtResults = new DataTable();

                _dtResults = QueryDataTable("tbl", "SP_GET_SPCRITERIADELSALECATE", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterSpCate);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CHANL_WISE_SALES> getSpecialCriSalesCatePY(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, DateTime preFrmDt, DateTime preToDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("p_prefdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = preFrmDt.Date;
                (param[4] = new OracleParameter("p_pretdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = preToDt.Date;
                (param[5] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[6] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[7] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[8] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[9] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[10] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[11] = new OracleParameter("p_curyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = frmDt.Year;
                (param[12] = new OracleParameter("p_preyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = preFrmDt.Year;
                DataTable _dtResults = new DataTable();
                _dtResults = QueryDataTable("tbl", "SP_GET_SPCRITERIADELPY", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterSpPy);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CHANL_WISE_SALES> getSpecialCriSalesPyWithCate(string comlst, string Channel, string BrandMngr, string Brand, string txtModel, string txtItem, DateTime frmDt, DateTime toDt, DateTime preFrmDt, DateTime preToDt, string filterby, string Category)
        {
            try
            {
                List<CHANL_WISE_SALES> sales = new List<CHANL_WISE_SALES>();
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt.Date;
                (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt.Date;
                (param[3] = new OracleParameter("p_prefdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = preFrmDt.Date;
                (param[4] = new OracleParameter("p_pretdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = preToDt.Date;
                (param[5] = new OracleParameter("IN_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[6] = new OracleParameter("IN_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[7] = new OracleParameter("IN_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[8] = new OracleParameter("IN_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comlst;
                (param[9] = new OracleParameter("IN_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[10] = new OracleParameter("IN_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category;
                (param[11] = new OracleParameter("p_curyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = frmDt.Year;
                (param[12] = new OracleParameter("p_preyr", OracleDbType.Int32, null, ParameterDirection.Input)).Value = preFrmDt.Year;
                DataTable _dtResults = new DataTable();
                _dtResults = QueryDataTable("tbl", "SP_GET_SPCRITERIADELPYCATE", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<CHANL_WISE_SALES>(_dtResults, CHANL_WISE_SALES.ConverterSpPyCate);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BI_TRGT_SALE> getTargetSaleData(DateTime startDtCurrent, DateTime endDtCurrent, DateTime preYerdtStart, DateTime preYerdtEnd, string defby, string catedifon, string calccd, string pdcd)
        {
            try
            {
                List<BI_TRGT_SALE> sales = new List<BI_TRGT_SALE>();
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_defby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = defby;
                (param[2] = new OracleParameter("p_catdefon", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = catedifon;
                (param[3] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = startDtCurrent;
                (param[4] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = endDtCurrent;
                (param[5] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = preYerdtStart;
                (param[6] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = preYerdtEnd;
                (param[7] = new OracleParameter("p_calcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = calccd;
                (param[8] = new OracleParameter("p_pdcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pdcd;
                DataTable _dtResults = new DataTable();

                _dtResults = QueryDataTable("tbl", "SP_GET_BITARGETSALEDATA", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    sales = DataTableExtensions.ToGenericList<BI_TRGT_SALE>(_dtResults, BI_TRGT_SALE.Converter);
                }
                return sales;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCostAnalysisData(string company, DateTime fromdt, DateTime todt, string type)
        {
            try
            {
                string det = "";
                DataTable DESCRPT = new DataTable();
                OracleParameter[] paramts = new OracleParameter[5];
                paramts[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (paramts[1] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt;
                (paramts[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt;
                (paramts[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (paramts[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                DESCRPT = QueryDataTable("tbl", "SP_GET_ELEMENTDETAILS", CommandType.StoredProcedure, false, paramts);
                if (DESCRPT.Rows.Count > 0)
                {
                    foreach (DataRow dtr in DESCRPT.Rows)
                    {
                        det += "'" + dtr["MSSE_DESC"].ToString() + "'" + ",";
                    }
                    det = det.Remove(det.Length - 1);
                }
                DataTable _dtResults = new DataTable("tbl");
                if (det != "")
                {
                    string query = "  SELECT * FROM(SELECT " +
                        "ib_mfile_no \"MANUAL FILE NO\"," +
                        "ib_bl_ref_no ||' - ' ||ib_si_seq_no \"LC NO\"," +
                        " (SELECT ibc_tp FROM imp_bl_container where ibc_desc=iced_ref and ibc_doc_no=ib_doc_no and ibc_act=1) \"CON.TYPE\",iced_ref \"CONTAINER NO\"," +
                       " ib_doc_clear_dt \"SHIPMENT CLEARED DATE\"," +
                        " iced_veh_reg \"VEHICLE NO\"," +
                        " icer_ref_no \"TRANSPORT INVOICE NO\",mbe_name \"TRANSPORTER\" ," +
                        " iced_cre_dt \"INVOICE ENTERING DATE\",iced_amt, /*icer_ref_amt \"AMOUNT\",*/" +
                        " msse_desc \"CODE\" FROM imp_cst_ele_ref " +
                        " inner join imp_bl_hdr on icer_seq_no=ib_seq_no" +
                        " left join IMP_CST_ELEREF_DET on icer_seq_no=iced_seq and iced_ele_line=icer_ele_line and iced_ref_line=icer_line and iced_act=1" +
                        " INNER JOIN mst_cost_segment ON iced_cd=msse_cd" +
                        " inner join mst_busentity on mbe_cd=icer_ser_provider and mbe_com='" + company + "' " +
                        " inner join MST_COST_ELE on mcae_ele_cat='" + type + "' and mcae_com='" + company + "' and mcae_cd=iced_cd" +
                        " where ib_com='" + company + "' and icer_doc_no='" + type + "'  and iced_cre_dt between to_date('" + fromdt.ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and to_date('" + todt.ToString("yyyy/MM/dd") + "','yyyy/mm/dd'))" +
                        " " +
                        " PIVOT(  sum(iced_amt)  FOR CODE IN (" + det + " ))  ORDER BY \"INVOICE ENTERING DATE\"";

                    OracleParameter[] param = new OracleParameter[2];
                    param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    (param[1] = new OracleParameter("p_query", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = query;
                    /*(param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt;
                    (param[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                    (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;*/
                    _dtResults = QueryDataTable("tbl", "SP_GET_COSTANALYSTDATA", CommandType.StoredProcedure, false, param);

                }
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getCostAnalysisDataSummery(string company, DateTime fromdt, DateTime todt, string type)
        {
            try
            {
                string det = "";
                DataTable DESCRPT = new DataTable();
                OracleParameter[] paramts = new OracleParameter[5];
                paramts[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (paramts[1] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt;
                (paramts[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt;
                (paramts[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (paramts[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                DESCRPT = QueryDataTable("tbl", "SP_GET_ELEMENTDETAILS", CommandType.StoredProcedure, false, paramts);
                if (DESCRPT.Rows.Count > 0)
                {
                    foreach (DataRow dtr in DESCRPT.Rows)
                    {
                        det += "'" + dtr["MSSE_DESC"].ToString() + "'" + ",";
                    }
                    det = det.Remove(det.Length - 1);
                }
                DataTable _dtResults = new DataTable("tbl");

                string select = "";
                if (DESCRPT.Rows.Count > 0)
                {
                    foreach (DataRow dtr in DESCRPT.Rows)
                    {
                        select += "sum(\"'" + dtr["MSSE_DESC"].ToString() + "'\") \"" + dtr["MSSE_DESC"].ToString() + "\",";
                    }
                    select = select.Remove(select.Length - 1);
                }
                if (det != "")
                {
                    string query = "SELECT  \"TRANSPORTER\"/*,sum(amount) AMOUNT*/," + select + " FROM (" +
                        " SELECT * FROM(SELECT " +
                        " mbe_name \"TRANSPORTER\" ," +
                        " iced_amt totamt, icer_ref_amt \"AMOUNT\", msse_desc \"CODE\" " +
                        " FROM imp_cst_ele_ref inner join imp_bl_hdr on icer_seq_no=ib_seq_no " +
                        " left join IMP_CST_ELEREF_DET on icer_seq_no=iced_seq and iced_ele_line=icer_ele_line and iced_ref_line=icer_line and iced_act=1 " +
                        " INNER JOIN mst_cost_segment ON iced_cd=msse_cd " +
                        " inner join mst_busentity on mbe_cd=icer_ser_provider and mbe_com='" + company + "' " +
                        " inner join MST_COST_ELE on mcae_ele_cat='" + type + "' and mcae_com='" + company + "' and mcae_cd=iced_cd where ib_com='" + company + "' " +
                        " and icer_doc_no='" + type + "'  and iced_cre_dt between to_date('" + fromdt.ToString("yyyy/MM/dd") + "','yyyy/mm/dd') and to_date('" + todt.ToString("yyyy/MM/dd") + "','yyyy/mm/dd')) " +
                        " PIVOT(  sum(totamt)  FOR CODE IN (" + det + " )))   group by \"TRANSPORTER\"";

                    OracleParameter[] param = new OracleParameter[2];
                    param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    (param[1] = new OracleParameter("p_query", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = query;
                    _dtResults = QueryDataTable("tbl", "SP_GET_COSTANALYSTDATA", CommandType.StoredProcedure, false, param);

                }
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MST_SEGMANT> getSegmantList(string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSSE_CD";
                    break;
                case "Description":
                    searchFld = "MSSE_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }

            List<MST_SEGMANT> cuslist = new List<MST_SEGMANT>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_COSTSEGMANTS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                cuslist = DataTableExtensions.ToGenericList<MST_SEGMANT>(_dtResults, MST_SEGMANT.Converter);
            }
            return cuslist;

        }
        public DataTable getBrandMgrAlloc(string _com, string _man, string cat1, string brand)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _man;
            (param[2] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat1;
            (param[3] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_brnd_alloc", CommandType.StoredProcedure, false, param);
            return _itemTable;
        }



        public List<BOND_BALANCE> getBondBalanceDetails(string brand, string cate1, string cate2, string cate3, string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                List<BOND_BALANCE> res = new List<BOND_BALANCE>();
                (param[0] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
                (param[2] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate1;
                (param[3] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate2;
                (param[4] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate3;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _itemTable = new DataTable();
                _itemTable = QueryDataTable("tblserial", "sp_get_bondbalancedetails", CommandType.StoredProcedure, false, param);
                if (_itemTable.Rows.Count > 0)
                {
                    res = DataTableExtensions.ToGenericList<BOND_BALANCE>(_itemTable, BOND_BALANCE.ConverterSub);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable runBondBalanceDetails(string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _itemTable = new DataTable();
                _itemTable = QueryDataTable("tblserial", "sp_get_bond_balance1bitl", CommandType.StoredProcedure, false, param);

                return _itemTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getLastRunTimeAlt(string code)
        {
            try
            {
                string res = string.Empty;
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _itemTable = new DataTable();
                _itemTable = QueryDataTable("tblserial", "SP_GET_LASTRUNTIME", CommandType.StoredProcedure, false, param);
                if (_itemTable.Rows.Count > 0)
                {
                    res = (_itemTable.Rows[0]["MMC_LAST_RUN_DT"] != DBNull.Value) ? Convert.ToDateTime(_itemTable.Rows[0]["MMC_LAST_RUN_DT"].ToString()).ToString("dd/MMM/yyyy HH:mm:ss ") : "";
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //dilshan sales inventory analysis
        public List<SalesInventoryAge> getsalesinventoryage(string selectedcompany, DateTime fromdate, DateTime todate, string user, string status)
        {
            try
            {
                List<SalesInventoryAge> _com = new List<SalesInventoryAge>();
                OracleParameter[] param = new OracleParameter[6];

                //(param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_selectedcompany", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
                (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate.Date;
                (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate.Date;
                (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[5] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;

                DataTable result = QueryDataTable("tbl", "sp_salesinventory_repnew", CommandType.StoredProcedure, false, param);
                if (result.Rows.Count > 0)
                {
                    _com = DataTableExtensions.ToGenericList<SalesInventoryAge>(result, SalesInventoryAge.Converter);
                }
                return _com;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //dilshan GP Analysis
        public List<GPAnalysis> getdeliveredsales(string selectedcompany, DateTime fromdate, DateTime todate, string user, string itemModel, string itemCode, string brand, string mainCat, string Category2, string filterby)
        {
            List<GPAnalysis> _com = new List<GPAnalysis>();
            OracleParameter[] param = new OracleParameter[11];

            //(param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_selectedcompany", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate.Date;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate.Date;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[5] = new OracleParameter("p_itemModel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemModel;
            (param[6] = new OracleParameter("p_itemCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[7] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            (param[8] = new OracleParameter("p_mainCat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mainCat;
            (param[9] = new OracleParameter("p_Category2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category2;
            (param[10] = new OracleParameter("p_foc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;


            DataTable res = QueryDataTable("tbl", "sp_get_delivered_sales_ccrdnew", CommandType.StoredProcedure, false, param);
            if (res.Rows.Count > 0)
            {
                _com = DataTableExtensions.ToGenericList<GPAnalysis>(res, GPAnalysis.Converter);
            }
            return _com;
        }
        //dilshan on 08/01/2019
        public List<ITService> getitservicedetails(string selectedcompany, DateTime fromdate, DateTime todate, string user, string itemModel, string itemCode, string brand, string mainCat, string Category2, string filterby)
        {
            List<ITService> _com = new List<ITService>();
            OracleParameter[] param = new OracleParameter[11];

            //(param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_selectedcompany", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = selectedcompany;
            (param[2] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate.Date;
            (param[3] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate.Date;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[5] = new OracleParameter("p_itemModel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemModel;
            (param[6] = new OracleParameter("p_itemCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[7] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            (param[8] = new OracleParameter("p_mainCat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mainCat;
            (param[9] = new OracleParameter("p_Category2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Category2;
            (param[10] = new OracleParameter("p_foc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;


            DataTable res = QueryDataTable("tbl", "sp_get_it_service_details", CommandType.StoredProcedure, false, param);
            if (res.Rows.Count > 0)
            {
                _com = DataTableExtensions.ToGenericList<ITService>(res, ITService.Converter);
            }
            return _com;
        }

        public List<MST_CHNL_SEARH_HEAD> getSearchChannelList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_CHNL_SEARH_HEAD> crg = new List<MST_CHNL_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_CHANLLIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_CHNL_SEARH_HEAD>(_dtResults, MST_CHNL_SEARH_HEAD.Converter);
            }
            return crg;
        }


        public List<MST_HIC_SEARH_HEAD> getSearchSubChannelList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_HIC_SEARH_HEAD> crg = new List<MST_HIC_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_SUBCHANLLIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return crg;
        }
        public List<MST_HIC_SEARH_HEAD> getSearchAreaList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_HIC_SEARH_HEAD> crg = new List<MST_HIC_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[6] = new OracleParameter("P_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_AREALIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return crg;
        }

        public List<MST_HIC_SEARH_HEAD> getSearchRegionList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_HIC_SEARH_HEAD> crg = new List<MST_HIC_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[6] = new OracleParameter("P_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            (param[7] = new OracleParameter("P_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_REGIONLIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return crg;
        }
        public List<MST_HIC_SEARH_HEAD> getSearchZoneList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area, string region)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_HIC_SEARH_HEAD> crg = new List<MST_HIC_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[6] = new OracleParameter("P_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            (param[7] = new OracleParameter("P_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[8] = new OracleParameter("P_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_ZONELIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return crg;
        }
        public List<MST_HIC_SEARH_HEAD> getSearchPCList(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string chnl, string schnl, string area, string region, string zone)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_HIC_SEARH_HEAD> crg = new List<MST_HIC_SEARH_HEAD>();
            OracleParameter[] param = new OracleParameter[11];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
            (param[6] = new OracleParameter("P_schnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
            (param[7] = new OracleParameter("P_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[8] = new OracleParameter("P_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[9] = new OracleParameter("P_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;

            param[10] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_SEARCH_PCLIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return crg;
        }
        public DataTable getCustomerSalesPcList(string company, string Channel, string Subchnl, string area, string Region, string Zone, string pc, string user)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[3] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[4] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
                (param[5] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Region;
                (param[6] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Zone;
                param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tblcrg", "SP_GET_INVOICEPCLIST", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getInventoryTurnOverDetails(DateTime fromdt, DateTime todt, string stus, string company, Int32 col)
        {
            try
            {
                List<INVTURNOVR> crg = new List<INVTURNOVR>();
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt.Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt.Date;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[4] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stus;
                (param[5] = new OracleParameter("p_col", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = col.ToString();
                _dtResults = QueryDataTable("tblcrg", "SP_GET_INVENTORYTURNOVERDET", CommandType.StoredProcedure, false, param);
                DataTable dtb = new DataTable("tb");
                dtb.Columns.Add("BRAND", typeof(string));
                dtb.Columns.Add("CATEGORY", typeof(string));
                dtb.Columns.Add("BMS_D_QTY" + col.ToString(), typeof(decimal));
                dtb.Columns.Add("BMS_D_COST" + col.ToString(), typeof(decimal));
                dtb.Columns.Add("BMS_D_NET_AMT" + col.ToString(), typeof(decimal));
                if (_dtResults.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtResults.Rows)
                    {
                        dtb.Rows.Add(dr.ItemArray);
                    }
                }
                //if (_dtResults.Rows.Count > 0)
                //{
                //    crg = DataTableExtensions.ToGenericList<INVTURNOVR>(_dtResults, INVTURNOVR.Converter);
                //}
                return dtb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getInventoryMonthEndBalance(DateTime fromdt, DateTime todt, string stus, string company, Int32 col)
        {
            try
            {
                List<INVTURNOVRBALANCE> crg = new List<INVTURNOVRBALANCE>();
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt.Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt.Date;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[4] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stus;
                (param[5] = new OracleParameter("p_col", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = col.ToString();
                _dtResults = QueryDataTable("tblcrg", "SP_GET_INVENTORYTURNOVERMNTBAL", CommandType.StoredProcedure, false, param);
                DataTable dtb = new DataTable("tb");
                dtb.Columns.Add("BRAND", typeof(string));
                dtb.Columns.Add("CATEGORY", typeof(string));
                //dtb.Columns.Add("QTY" + col.ToString(), typeof(decimal));
                dtb.Columns.Add("COST" + col.ToString(), typeof(decimal));
                if (_dtResults.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtResults.Rows)
                    {
                        dtb.Rows.Add(dr.ItemArray);
                    }
                }
                //if (_dtResults.Rows.Count > 0)
                //{
                //    crg = DataTableExtensions.ToGenericList<INVTURNOVRBALANCE>(_dtResults, INVTURNOVRBALANCE.Converter);
                //}
                return dtb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getInventoryAvgCost(DateTime fromdt, DateTime todt, string stus, string company, int mntcnt, int cnt)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt.Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt.Date;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[4] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stus;
                (param[5] = new OracleParameter("p_mntcnt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mntcnt;
                _dtResults = QueryDataTable("tblcrg", "SP_GET_INVENTORYTURNOVEAVGTOT", CommandType.StoredProcedure, false, param);
                DataTable dtb = new DataTable("tb");
                dtb.Columns.Add("BRAND", typeof(string));
                dtb.Columns.Add("CATEGORY", typeof(string));
                dtb.Columns.Add("TOT_COST" + cnt.ToString(), typeof(decimal));
                dtb.Columns.Add("AVG_COST" + cnt.ToString(), typeof(decimal));
                if (_dtResults.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtResults.Rows)
                    {
                        dtb.Rows.Add(dr.ItemArray);
                    }
                }
                //if (_dtResults.Rows.Count > 0)
                //{
                //    crg = DataTableExtensions.ToGenericList<INVTURNOVRBALANCE>(_dtResults, INVTURNOVRBALANCE.Converter);
                //}
                return dtb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BOND_BALANCE_AAL> getBondBalanceDetailsAAL(string userid)
        {
            try
            {

                List<BOND_BALANCE_AAL> crg = new List<BOND_BALANCE_AAL>();
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                param[1] = new OracleParameter("p_brand", OracleDbType.NVarchar2, userid, ParameterDirection.Input);
                DataTable _dtResults = QueryDataTable("tblcrg", "sp_getbitlaalbndbal", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<BOND_BALANCE_AAL>(_dtResults, BOND_BALANCE_AAL.ConverterSub);
                }
                return crg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 addTemporyParameters(string userid, string type, string report, string value)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[1] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = value;
                (param[2] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;
                (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int16)UpdateRecords("SP_ADD_PARAMETERVLUES", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 rempveTemporyParameters(string userid, string report)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;
                (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int16)UpdateRecords("SP_DELETE_PARAMETERVLUES", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Dulaj Mar 27
        public List<CUSTOMER_SALES> getCustomerDetails(DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string company,
           string pclst, string MainCat, string Brand, string txtModel, string txtItem, string filterby, string cat2,
           string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype,
           string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype,
           string Channel, string Subchnl, string Area, string Region, string Zone, string pc, string user, string dist, string prov, string CheckMobile)
        {
            try
            {
                List<CUSTOMER_SALES> finres = new List<CUSTOMER_SALES>();
                OracleParameter[] param = new OracleParameter[33];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesFrom.ToString("dd/MMM/yyyy")).Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesTo.ToString("dd/MMM/yyyy")).Date;
                (param[3] = new OracleParameter("p_chkamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = CheckAmount;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[6] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[7] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Area;
                (param[8] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[9] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[10] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[11] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[12] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[13] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[14] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[15] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MainCat;
                (param[16] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
                (param[17] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat3;
                (param[18] = new OracleParameter("p_reptype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;
                (param[19] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                (param[20] = new OracleParameter("p_numberofvisit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = visit;
                (param[21] = new OracleParameter("p_age", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = age;
                (param[22] = new OracleParameter("p_salary", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = salary;
                (param[23] = new OracleParameter("p_custown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                (param[24] = new OracleParameter("p_purctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                (param[25] = new OracleParameter("p_saletype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                (param[26] = new OracleParameter("p_schematype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                (param[27] = new OracleParameter("p_schemacd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                (param[28] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;
                (param[29] = new OracleParameter("p_crdcdbnk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                (param[30] = new OracleParameter("p_dist", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dist;
                (param[31] = new OracleParameter("p_prov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prov;
                (param[32] = new OracleParameter("p_mobtyp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CheckMobile;

                //(param[9] = new OracleParameter("p_itemcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                //(param[10] = new OracleParameter("p_filterby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;

                //(param[13] = new OracleParameter("p_visit", OracleDbType.Int32, null, ParameterDirection.Input)).Value = visit;
                //(param[14] = new OracleParameter("p_age", OracleDbType.Int32, null, ParameterDirection.Input)).Value = age;
                //(param[16] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                //(param[17] = new OracleParameter("p_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                //(param[18] = new OracleParameter("P_schemetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                //(param[19] = new OracleParameter("P_schemecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                //(param[20] = new OracleParameter("p_ctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                //(param[21] = new OracleParameter("p_ptown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                //(param[22] = new OracleParameter("p_bankcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                //(param[23] = new OracleParameter("p_withserial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Withserial;
                //(param[24] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;

                DataTable _dtResults = QueryDataTable("tblcrg", "customer_details_bi_new", CommandType.StoredProcedure, false, param);
                //DataTable _dtResults = QueryDataTable("tblcrg", "pkg_dashboard.SP_GET_CUSTOMERSALES_NEW", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    //if (filterby == "item")
                    //{
                    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.CustomerDetails);
                    //}
                    //else
                    //{
                    //    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.ConverterSub);
                    //}
                }

                return finres;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CUSTOMER_SALES> getInvDetails(DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string company,
           string pclst, string MainCat, string Brand, string txtModel, string txtItem, string filterby, string cat2,
           string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype,
           string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype,
           string Channel, string Subchnl, string Area, string Region, string Zone, string pc, string user, string dist, string prov)
        {
            try
            {
                List<CUSTOMER_SALES> finres = new List<CUSTOMER_SALES>();
                OracleParameter[] param = new OracleParameter[32];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesFrom.ToString("dd/MMM/yyyy")).Date;
                (param[2] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(SalesTo.ToString("dd/MMM/yyyy")).Date;
                (param[3] = new OracleParameter("p_chkamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = CheckAmount;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Channel;
                (param[6] = new OracleParameter("p_subchnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[7] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Area;
                (param[8] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[9] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Subchnl;
                (param[10] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[11] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[12] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                (param[13] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[14] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[15] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MainCat;
                (param[16] = new OracleParameter("p_cat2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat2;
                (param[17] = new OracleParameter("p_cat3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat3;
                (param[18] = new OracleParameter("p_reptype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = filterby;
                (param[19] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = customer;
                (param[20] = new OracleParameter("p_numberofvisit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = visit;
                (param[21] = new OracleParameter("p_age", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = age;
                (param[22] = new OracleParameter("p_salary", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = salary;
                (param[23] = new OracleParameter("p_custown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CTown;
                (param[24] = new OracleParameter("p_purctown", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PTown;
                (param[25] = new OracleParameter("p_saletype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invtype;
                (param[26] = new OracleParameter("p_schematype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemetype;
                (param[27] = new OracleParameter("p_schemacd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schemecode;
                (param[28] = new OracleParameter("p_paytype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Paymenttype;
                (param[29] = new OracleParameter("p_crdcdbnk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BankCode;
                (param[30] = new OracleParameter("p_dist", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dist;
                (param[31] = new OracleParameter("p_prov", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prov;

                DataTable _dtResults = QueryDataTable("tblcrg", "SP_GET_CUSTOMERSALES_NEW", CommandType.StoredProcedure, false, param);


                if (_dtResults.Rows.Count > 0)
                {

                    finres = DataTableExtensions.ToGenericList<CUSTOMER_SALES>(_dtResults, CUSTOMER_SALES.ConverterAll);

                }

                return finres;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MST_HIC_SEARH_HEAD> getRelatedPclist(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string channel, string userId, Int32 haspcper)
        {
            searchVal = "%" + searchVal + "%";
            switch (searchFld)
            {
                case "Code":
                    searchFld = "MSC_CD";
                    break;
                case "Description":
                    searchFld = "MSC_DESC";
                    break;
                default:
                    searchFld = "";
                    break;

            }

            List<MST_HIC_SEARH_HEAD> cuslist = new List<MST_HIC_SEARH_HEAD>(); ;
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal;
            (param[4] = new OracleParameter("P_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[6] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            (param[7] = new OracleParameter("p_hasper", OracleDbType.Int32, null, ParameterDirection.Input)).Value = haspcper;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_BUGPCLIST", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                cuslist = DataTableExtensions.ToGenericList<MST_HIC_SEARH_HEAD>(_dtResults, MST_HIC_SEARH_HEAD.Converter);
            }
            return cuslist;
        }

        public List<GLB_PROFITABILITY> getProfitabilityData(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_PRFITABILITYDATA", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GLB_PROFITABILITY> getProfitabilityDetails(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user, Int32 headid, Int32 groupid)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();
                OracleParameter[] param = new OracleParameter[12];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_hedid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = headid;
                (param[8] = new OracleParameter("p_grpid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = groupid;
                (param[9] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[10] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_PRFITABILITYDETAILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<USR_DEF_TEMP> getUsrTemplate(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type)
        {
            string search = "";
            if (!string.IsNullOrEmpty(searchFld))
            {
                search = searchFld + "%";
            }

            List<USR_DEF_TEMP> crg = new List<USR_DEF_TEMP>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_upd_tmp_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[3] = new OracleParameter("p_upd_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal;
            (param[4] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            //  (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = desc;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_usr_def_ser", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<USR_DEF_TEMP>(_dtResults, USR_DEF_TEMP.Converter);
            }
            return crg;
        }
        public List<GLB_PROFITABILITY> getProfitabilityPcDetails(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user, Int32 headid, Int32 groupid, Int32 eleid)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();
                OracleParameter[] param = new OracleParameter[13];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_hedid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = headid;
                (param[8] = new OracleParameter("p_grpid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = groupid;
                (param[9] = new OracleParameter("p_eleid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = eleid;
                (param[10] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[11] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[12] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_PRFITABILITYPRFDET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getInventorySerialAge(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, string agefrom, string ageto, string agefromto)
        {
            OracleParameter[] param = new OracleParameter[20];
            (param[0] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[1] = new OracleParameter("p_brnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[2] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[3] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[7] = new OracleParameter("p_itemCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[8] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[9] = new OracleParameter("p_inclution", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inclution;
            (param[10] = new OracleParameter("p_cate1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate1);
            (param[11] = new OracleParameter("p_cate2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate2);
            (param[12] = new OracleParameter("p_cate3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate3);
            (param[13] = new OracleParameter("p_brandMngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brandMngr;
            (param[14] = new OracleParameter("p_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(stus);
            (param[15] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[16] = new OracleParameter("p_agefrom", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(agefrom);
            (param[17] = new OracleParameter("p_ageto", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(ageto);
            (param[18] = new OracleParameter("p_agefromto", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agefromto;
            param[19] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_inventory_ser_age", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getInventorySerialAgeHeading(string column, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_inventory_ser_agehd", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getImportSerialAgeHeading(string column, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_import_ser_agehd", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getInventorySerialAgeHeadingCol(string column, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_inventory_ser_agehdcol", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getImportSerialAgeHeadingCol(string column, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_import_ser_agehdcol", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getInventorySerialAgeAsAt(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, DateTime asAtDtStrng, string agefrom, string ageto, string agefromto, DateTime asAtDtStrngfrom, DateTime asAtDtStrngto, string userId)
        {
            OracleParameter[] param = new OracleParameter[24];
            (param[0] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[1] = new OracleParameter("p_brnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[2] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[3] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[7] = new OracleParameter("p_itemCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[8] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[9] = new OracleParameter("p_inclution", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inclution;
            (param[10] = new OracleParameter("p_cate1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate1);
            (param[11] = new OracleParameter("p_cate2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate2);
            (param[12] = new OracleParameter("p_cate3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate3);
            (param[13] = new OracleParameter("p_brandMngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brandMngr;
            (param[14] = new OracleParameter("p_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(stus);
            (param[15] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[16] = new OracleParameter("p_asatdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = asAtDtStrng;
            (param[17] = new OracleParameter("p_agefrom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToInt32(agefrom);
            (param[18] = new OracleParameter("p_ageto", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Convert.ToInt32(ageto);
            (param[19] = new OracleParameter("p_agefromto", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agefromto;
            (param[20] = new OracleParameter("P_asatdatefrom", OracleDbType.Date, null, ParameterDirection.Input)).Value = asAtDtStrngfrom;
            (param[21] = new OracleParameter("P_asatdateto", OracleDbType.Date, null, ParameterDirection.Input)).Value = asAtDtStrngto;
            (param[22] = new OracleParameter("P_userId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[23] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_inventory_ser_age_asat", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getImportSerialAgeAsAt(string _cat1, string _brnd, string locHircCd, string locHircDesc, string status, string type, string model, string itemCd, string company, string inclution, bool cate1, bool cate2, bool cate3, string brandMngr, bool stus, string column, List<BRND_NEW_STUS> stusForTyp, string asAtDtStrng, string asAtDtfrom, string asAtDtto, string grnno, string tobondno, string serialno, string userId)
        {
            OracleParameter[] param = new OracleParameter[24];
            (param[0] = new OracleParameter("p_cat1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat1;
            (param[1] = new OracleParameter("p_brnd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brnd;
            (param[2] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[3] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[5] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[6] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[7] = new OracleParameter("p_itemCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[8] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[9] = new OracleParameter("p_inclution", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = inclution;
            (param[10] = new OracleParameter("p_cate1", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate1);
            (param[11] = new OracleParameter("p_cate2", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate2);
            (param[12] = new OracleParameter("p_cate3", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(cate3);
            (param[13] = new OracleParameter("p_brandMngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brandMngr;
            (param[14] = new OracleParameter("p_stus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(stus);
            (param[15] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[16] = new OracleParameter("p_asatdate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = asAtDtStrng;
            (param[17] = new OracleParameter("P_asatdatefrom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = asAtDtfrom;
            (param[18] = new OracleParameter("P_asatdateto", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = asAtDtto;
            (param[19] = new OracleParameter("P_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = grnno;
            (param[20] = new OracleParameter("P_tobondno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tobondno;
            (param[21] = new OracleParameter("P_serialno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serialno;
            (param[22] = new OracleParameter("P_userId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[23] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_import_ser_age_asat", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<GV_SEARCH_HEAD> GetItemByType(string pgeNum, string pgeSize, string searchFld, string searchVal, string company, string type)
        {
            List<GV_SEARCH_HEAD> gv = new List<GV_SEARCH_HEAD>();
            string _item = null;
            string _desc = null;
            string _model = null;

            if (!string.IsNullOrEmpty(searchFld))
            {
                switch (searchVal.ToUpper())
                {
                    case "ITEM":
                        _item = searchVal.ToUpper();
                        break;

                    case "DESCRIPTION":
                        _desc = searchVal.ToUpper();
                        break;

                    case "MODEL":
                        _model = searchVal.ToUpper();
                        break;

                    default:
                        break;
                }
            }
            _item = (_item != null) ? (_item.ToUpper() + "%") : "";
            _desc = (_desc != null) ? (_desc.ToUpper() + "%") : "";
            _model = (_model != null) ? (_model.ToUpper() + "%") : "";
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;

            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.ToString();
            (param[3] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _desc.ToString();
            (param[4] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model.ToString();
            (param[5] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pgeNum);
            (param[6] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(pgeSize);
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "SP_SEARCH_GIFTVOUCHERBI", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                gv = DataTableExtensions.ToGenericList<GV_SEARCH_HEAD>(_dtResults, GV_SEARCH_HEAD.Converter);
            }
            return gv;
        }

        public Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber, Int32 number)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[9];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_moduleid;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_start_char;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_cd;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_modify_dt;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_year;
            (param[7] = new OracleParameter("p_autno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = number;


            param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_updateautonumberspecial", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        //Pasindu 2018/07/11
        public List<INVOICE_MAIN_TYPE> getInvoiceTypesMainTp()
        {
            List<INVOICE_MAIN_TYPE> gv = new List<INVOICE_MAIN_TYPE>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblmainTp", "SP_GET_INVOICE_TYPES_MAINTP", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                gv = DataTableExtensions.ToGenericList<INVOICE_MAIN_TYPE>(_dtResults, INVOICE_MAIN_TYPE.Converter);
            }
            return gv;
        }

        //Pasindu 2018/07/12
        public List<MAIN_CAT2_SEARCH> getCategorySubSearch(string pgeNum, string pgeSize, string searchFld, string searchVal, string cate)
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

            List<MAIN_CAT2_SEARCH> crg = new List<MAIN_CAT2_SEARCH>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_cat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblsubct", "SP_GET_SUBCATEGORY_BYCAT", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                crg = DataTableExtensions.ToGenericList<MAIN_CAT2_SEARCH>(_dtResults, MAIN_CAT2_SEARCH.Converter2);
            }
            return crg;
        }

        //Pasindu 2018/07/16
        public List<SEARCH_FAST_MOVING_DET> getFMIDetails(string p_company, DateTime p_fromdate, DateTime p_todate)
        {
            try
            {
                List<SEARCH_FAST_MOVING_DET> det = new List<SEARCH_FAST_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_fromdate;
                (param[2] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_todate;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_VIEW_FAST_MOVINGITM_DET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_FAST_MOVING_DET>(_dtResults, SEARCH_FAST_MOVING_DET.Converter2);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Pasindu 2018/07/16
        public List<SEARCH_FAST_MOVING_DET> getFMIDetailsPRV(string p_company, DateTime p_prev_fromdate, DateTime p_prev_todate)
        {
            try
            {
                List<SEARCH_FAST_MOVING_DET> det = new List<SEARCH_FAST_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_prev_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_prev_fromdate;
                (param[2] = new OracleParameter("p_prev_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_prev_todate;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_VIEW_FAST_MOVINGITM_DET_PRV", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_FAST_MOVING_DET>(_dtResults, SEARCH_FAST_MOVING_DET.Converter4);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Pasindu 2018/07/18
        public List<PROVINCES_LIST> getProvinceList()
        {
            List<PROVINCES_LIST> gv = new List<PROVINCES_LIST>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblmainTp", "GET_PROVINCE_DET", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                gv = DataTableExtensions.ToGenericList<PROVINCES_LIST>(_dtResults, PROVINCES_LIST.Converter);
            }
            return gv;
        }


        //Pasindu 2018/07/22
        public List<SEARCH_SLOW_MOVING_DET> getSMIDetails(string p_company, DateTime p_fromdate, DateTime p_todate)
        {
            try
            {
                List<SEARCH_SLOW_MOVING_DET> det = new List<SEARCH_SLOW_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_fromdate;
                (param[2] = new OracleParameter("p_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_todate;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_VIEW_SLOW_MOVINGITM_DET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_SLOW_MOVING_DET>(_dtResults, SEARCH_SLOW_MOVING_DET.Converter2);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Pasindu 2018/07/22
        public List<SEARCH_SLOW_MOVING_DET> getSMIDetailsFromDate(string p_company, DateTime p_asatfromdate)
        {
            try
            {
                List<SEARCH_SLOW_MOVING_DET> det = new List<SEARCH_SLOW_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_asatfromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_asatfromdate;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SLOW_MOVINGITM_DET_INV_FROM", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_SLOW_MOVING_DET>(_dtResults, SEARCH_SLOW_MOVING_DET.Converter4);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Pasindu 2018/07/22
        public List<SEARCH_SLOW_MOVING_DET> getSMIDetailsToDate(string p_company, DateTime p_asattodate)
        {
            try
            {
                List<SEARCH_SLOW_MOVING_DET> det = new List<SEARCH_SLOW_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_asatfromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_asattodate;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SLOW_MOVINGITM_DET_INV_TO", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_SLOW_MOVING_DET>(_dtResults, SEARCH_SLOW_MOVING_DET.Converter3);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Pasindu 2018/07/22
        public List<SEARCH_FAST_MOVING_DET> getFMIDetailsPURD(string p_company, DateTime p_asattodate)
        {
            try
            {
                List<SEARCH_FAST_MOVING_DET> det = new List<SEARCH_FAST_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_asatfromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_asattodate;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRefpurd", "SP_FAST_MOVINGITM_DET_INV_PURD", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_FAST_MOVING_DET>(_dtResults, SEARCH_FAST_MOVING_DET.Converter3);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Pasindu 2018/07/25
        public List<SEARCH_FAST_MOVING_DET> getFMIDetailsArrivalOP(string p_company, DateTime p_asat)
        {
            try
            {
                List<SEARCH_FAST_MOVING_DET> det = new List<SEARCH_FAST_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_fromasat", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_asat;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_FAST_MOVINGITM_DET_INV_TO", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_FAST_MOVING_DET>(_dtResults, SEARCH_FAST_MOVING_DET.Converter3);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Pasindu 2018/07/25
        public List<AGING_DET> getAgingDetails(string p_company)
        {
            try
            {
                List<AGING_DET> det = new List<AGING_DET>();
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblAge", "GET_AGING_DETAILS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<AGING_DET>(_dtResults, AGING_DET.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2018-07-30
        public List<ProfitAnalicer> getItemPriceBI2(string Com, string Item, string PriceBook, string PriceLevel)
        {
            try
            {
                List<ProfitAnalicer> crg = new List<ProfitAnalicer>();
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
                (param[1] = new OracleParameter("P_CURRNTDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now.Date;
                (param[2] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Item;
                (param[3] = new OracleParameter("P_PRICEBOOK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PriceBook;
                (param[4] = new OracleParameter("P_PRICELEVEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PriceLevel;
                param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblcrg", "SP_GETBI_ITEM_PRICELT", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<ProfitAnalicer>(_dtResults, ProfitAnalicer.Converter);
                }
                return crg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Subodana 2018-07-25
        public decimal GetLatestCostForBI(string ItemCode, string Company)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ItemCode;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Company;
            param[2] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "SP_GET_LATESTCOSTFBI", CommandType.StoredProcedure, false, param);
            decimal val = 0;
            if (_dtResults != null && _dtResults.Rows.Count > 0 && _dtResults.Rows[0][0].ToString() != "")
            {
                val = Convert.ToDecimal(_dtResults.Rows[0][0].ToString());
            }
            return val;
        }
        //SUBODANA
        public List<ref_bud_ele_form> GetAccDynamicFields(string _company)
        {
            List<ref_bud_ele_form> _acc = new List<ref_bud_ele_form>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tbl = QueryDataTable("tblPb", "SP_GETDYNAMICACCFIELDS", CommandType.StoredProcedure, false, param);
            if (_tbl.Rows.Count > 0)
            {
                _acc = DataTableExtensions.ToGenericList<ref_bud_ele_form>(_tbl, ref_bud_ele_form.Converter);
            }
            return _acc;
        }
        public decimal GetInsTPVal(string com, string ele, DateTime _date, string _item, string _brand, string _maincat, string _cat, string btu, string model)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("P_CURRNTDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_ELE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ele;
            (param[3] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("P_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            (param[5] = new OracleParameter("P_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = model;
            (param[6] = new OracleParameter("P_MCAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _maincat;
            (param[7] = new OracleParameter("P_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[8] = new OracleParameter("P_BTU", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = btu;
            param[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbData", "SP_GET_INSTP_VAL", CommandType.StoredProcedure, false, param);
            decimal val = 0;
            if (_dtResults.Rows.Count > 0)
            {
                val = Convert.ToDecimal(_dtResults.Rows[0]["rbic_val"].ToString());
            }
            return val;
        }
        public DataTable getHPaccCompAsAt(string _schmcode, string _schmterm, string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, Int32 premonth, Int32 preyear)
        {
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("P_schemecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schmcode;
            (param[1] = new OracleParameter("P_schemeterm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schmterm;
            (param[2] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[3] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[4] = new OracleParameter("P_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_curmonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curmonth;
            (param[6] = new OracleParameter("P_curyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curyear;
            (param[7] = new OracleParameter("P_premonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = premonth;
            (param[8] = new OracleParameter("P_preyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = preyear;
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_hpacc_comparison_asat", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public List<HP_ACC_COMP> getHPaccCompAsAt_NEW(string _schmcode, string _schmterm, string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, Int32 premonth, Int32 preyear)
        {
            try
            {
                List<HP_ACC_COMP> crg = new List<HP_ACC_COMP>();
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("P_schemecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schmcode;
                (param[1] = new OracleParameter("P_schemeterm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _schmterm;
                (param[2] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
                (param[3] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
                (param[4] = new OracleParameter("P_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[5] = new OracleParameter("P_curmonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curmonth;
                (param[6] = new OracleParameter("P_curyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curyear;
                (param[7] = new OracleParameter("P_premonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = premonth;
                (param[8] = new OracleParameter("P_preyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = preyear;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tmpRepSer", "sp_get_hpacc_comparison_asat", CommandType.StoredProcedure, false, param);
                //_dtResults = QueryDataTable("tmpRepSer", "sp_get_comloccurr_age", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    crg = DataTableExtensions.ToGenericList<HP_ACC_COMP>(_dtResults, HP_ACC_COMP.ConverterSub);
                }
                return crg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BMT_REF_HEAD> getHPBIToolProperties(string _searchValue, string _pageNum, string _pageSize, string _serachField, string _propertyType)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "BMR_COL_NM";
                        break;
                    case "Description":
                        _serachField = "BMR_COL_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }

                List<BMT_REF_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_colDescription", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[3] = new OracleParameter("p_serachField", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[4] = new OracleParameter("p_propertyType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _propertyType;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_Search_HPBIToolProperties", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<BMT_REF_HEAD> LoadHPBIToolDetailsByName(string _columnName)
        {
            try
            {
                List<BMT_REF_HEAD> propertyList = null;
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_column_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _columnName;
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "pkg_dashboard.SP_GET_HPBITOOLPROPERTY_DETAIL", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    propertyList = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return propertyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable getHPDebtorsArrAsAt(string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, string userId)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[1] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[2] = new OracleParameter("P_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("P_curmonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curmonth;
            (param[4] = new OracleParameter("P_curyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curyear;
            (param[5] = new OracleParameter("P_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_hpdebtors_arr_asat", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public DataTable getHPDebtorsArrAsAtSum(string locHircCd, string locHircDesc, string company, Int32 curmonth, Int32 curyear, string userId)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_locHircCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircCd;
            (param[1] = new OracleParameter("p_locHircDesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locHircDesc;
            (param[2] = new OracleParameter("P_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[3] = new OracleParameter("P_curmonth", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curmonth;
            (param[4] = new OracleParameter("P_curyear", OracleDbType.Int32, null, ParameterDirection.Input)).Value = curyear;
            (param[5] = new OracleParameter("P_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "sp_get_hpdebtors_arr_sum", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        //dilshan 24/07/2018
        public DataTable GetItemDailyAVGDispatch_New(string _com, string _itm, string _cat, DateTime _dtFrom, DateTime _dtTo)
        {
            decimal _tmp = 0, _dec = 0;
            DataTable _dtResults = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_ith_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_itb_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[2] = new OracleParameter("p_ml_cate_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cat;
            (param[3] = new OracleParameter("p_dt_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            (param[4] = new OracleParameter("p_dt_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_bt_dw_get_itm_disp_avgnew", CommandType.StoredProcedure, false, param);
            if (_dtResults != null)
            {
                if (_dtResults.Rows.Count > 0)
                {
                    _dec = decimal.TryParse(_dtResults.Rows[0][0].ToString(), out _tmp) ? Convert.ToDecimal(_dtResults.Rows[0][0].ToString()) : 0;
                }
            }
            return _dtResults;
        }

        public DataTable getStockBalanceDetails(string querystring)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_query", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = querystring;
                _dtResults = QueryDataTable("tbl", "SP_GET_COSTANALYSTDATA", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getPnlFilAllDet(string grp, string cate, DateTime frmdt, DateTime todt, string accgrp)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_grp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = grp;
                (param[2] = new OracleParameter("p_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[3] = new OracleParameter("p_fromdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt.Date;
                (param[4] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt.Date;
                (param[5] = new OracleParameter("p_accgrp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = accgrp;
                _dtResults = QueryDataTable("tbl", "SP_GET_REPORTPCCATLST", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getQuertStringDetails(string querystring)
        {
            try
            {
                DataTable _dtResults = new DataTable();
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_query", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = querystring;
                _dtResults = QueryDataTable("tbl", "SP_GET_COSTANALYSTDATA", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BMT_HPTERM_HEAD> getHPTerem(string _searchValue, string _pageNum, string _pageSize, string _serachField)
        {
            try
            {
                switch (_serachField)
                {
                    case "Terms":
                        _serachField = "HSD_TERM";
                        break;
                    default:
                        _serachField = "";
                        break;

                }
                if (_searchValue == "%%")
                {
                    _searchValue = "";
                }

                List<BMT_HPTERM_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_HPTERMS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_HPTERM_HEAD>(_dtResults, BMT_HPTERM_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<HPSCHCATE_SEARCH> getHPSchCate(string _searchValue, string _pageNum, string _pageSize, string _serachField)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "HSC_CD";
                        break;
                    case "Description":
                        _serachField = "HSC_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }
                if (_searchValue == "%%")
                {
                    _searchValue = "";
                }

                List<HPSCHCATE_SEARCH> cuslist = null;
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_HPSCHCATE", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<HPSCHCATE_SEARCH>(_dtResults, HPSCHCATE_SEARCH.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public decimal getStockDeTAILS(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string cate, string com, string user)
        {
            try
            {
                decimal det = 0;
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_PRFITABILITYSTOCK", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["COST"] != DBNull.Value)
                {
                    det = Convert.ToDecimal(_dtResults.Rows[0]["COST"].ToString());
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDebtDTAILS(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string cate, string com, string user)
        {
            try
            {
                decimal det = 0;
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "";
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_PRFITABILITYDEBTBAL", CommandType.StoredProcedure, false, param);

                //if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["COST"] != DBNull.Value)
                //{
                //    det = Convert.ToDecimal(_dtResults.Rows[0]["COST"].ToString());
                //}
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int addTUserProfitCenters(string user, string type, string report, string group, string company)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[1] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;
                (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[3] = new OracleParameter("p_group", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = group;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int16)UpdateRecords("SP_ADD_PARAMETERVLUESUSER", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getUserRepaltedPcs(string user, string type, string report, string group, string company)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                Int32 effects = 0;
                (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                (param[1] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = report;
                (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[3] = new OracleParameter("p_group", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = group;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_USERPCLIST", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Dulaj 2018/Aug/22
        public List<RptWarehousStockBalance> getdfdpall(string _com, string _itm, DateTime _dtFrom, DateTime _dtTo)
        {
            DataTable _dtResults = new DataTable();
            List<RptWarehousStockBalance> _list = new List<RptWarehousStockBalance>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_ib_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            //(param[1] = new OracleParameter("p_ibi_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            //(param[2] = new OracleParameter("p_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            //(param[3] = new OracleParameter("p_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            //(param[4] = new OracleParameter("p_dt_1", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo.AddDays(1);
            //(param[5] = new OracleParameter("p_dt_2", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo.AddDays(2);
            //(param[6] = new OracleParameter("p_dt_3", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo.AddDays(3);
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tbl", "sp_get_all_dp_df", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<RptWarehousStockBalance>(_dtResults, RptWarehousStockBalance.ConverterDPDF);
            }
            if (_list != null)
            {
                if (_list.Count > 0)
                {
                    _list = _list.OrderByDescending(c => c.Is_df_loc).ToList();
                }
            }
            return _list;

        }

        public DataTable getItemdescription()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALITEMDESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getItemBrandDesc()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALBRANDDESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getItemModelDesc()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALMODELDESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getItemCate1Desc()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALCATE1DESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getItemCate2Desc()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALCATE2DESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getItemCate3Desc()
        {
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_ALCATE3DESC", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable asAtbalanceProcess(DateTime asatDt, string com, string location, string chnl, string schnl, string area, string region, string zone, string BrandMngr, string model, string brand, string item, string querystring, bool currdt)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[12];
                (param[0] = new OracleParameter("P_FRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = asatDt.Date;
                (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
                (param[4] = new OracleParameter("P_SUBCHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = schnl;
                (param[5] = new OracleParameter("P_AREA", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
                (param[6] = new OracleParameter("P_REGION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
                (param[7] = new OracleParameter("P_ZONE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
                (param[8] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
                (param[9] = new OracleParameter("P_CURRENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (currdt == true) ? 1 : 0;
                (param[10] = new OracleParameter("P_QERYSTRING", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = querystring;
                param[11] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_UPDATE_ASATSTOCKBALDET", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SEARCH_FAST_MOVING_DET> getFMIDetailsPRVN(string p_company, DateTime p_prev_fromdate, DateTime p_prev_todate)
        {
            try
            {
                List<SEARCH_FAST_MOVING_DET> det = new List<SEARCH_FAST_MOVING_DET>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_company;
                (param[1] = new OracleParameter("p_prev_fromdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_prev_fromdate;
                (param[2] = new OracleParameter("p_prev_todate", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_prev_todate;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_VIEW_FAST_MOVINGITM_DET_PRV", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<SEARCH_FAST_MOVING_DET>(_dtResults, SEARCH_FAST_MOVING_DET.Converter5);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HPSCHCATE_SEARCH> getBudgetElements(string _searchValue, string _pageNum, string _pageSize, string _serachField, string type)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "HSC_CD";
                        break;
                    case "Description":
                        _serachField = "HSC_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }
                if (_searchValue == "%%")
                {
                    _searchValue = "";
                }

                List<HPSCHCATE_SEARCH> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_BUDGETELEMENTS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<HPSCHCATE_SEARCH>(_dtResults, HPSCHCATE_SEARCH.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LOC_SEARCH> searchLocation(string _searchValue, string _pageNum, string _pageSize, string _serachField, string company)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "ML_LOC_CD";
                        break;
                    case "Description":
                        _serachField = "ML_LOC_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }
                if (_searchValue == "%%")
                {
                    _searchValue = "";
                }

                List<LOC_SEARCH> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_SEARCH_LOCATIONS", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<LOC_SEARCH>(_dtResults, LOC_SEARCH.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ADD BY THARANGA 2018/12/28
        public List<GLB_PROFITABILITY> getBudgetReport(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();
                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "sp_BudgetReport", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter1);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getBudgetReport_tb(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();

                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_cate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("p_pyfrmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("p_pytodt", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "sp_BudgetReport", CommandType.StoredProcedure, false, param);

                //if (_dtResults.Rows.Count > 0)
                //{
                //    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter1);
                //}
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string getBrandDesc(string brand)
        {
            string desc = "";
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_brandcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brand;
            DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_BRAND_DETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults != null && _dtResults.Rows.Count > 0)
            {
                if (_dtResults.Rows[0]["MB_DESC"] != DBNull.Value)
                    desc = _dtResults.Rows[0]["MB_DESC"].ToString();
            }
            return desc;
        }

        public string getCateDesc(string cate1, string cate2, string cate3, string type)
        {
            string desc = "";
            OracleParameter[] param = new OracleParameter[5];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_cate1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate1;
            (param[2] = new OracleParameter("in_cate2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate2;
            (param[3] = new OracleParameter("in_cate3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate3;
            (param[4] = new OracleParameter("in_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_CATE_DETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults != null && _dtResults.Rows.Count > 0)
            {
                desc = _dtResults.Rows[0]["DESCR"].ToString();
            }
            return desc;
        }
        public DataTable getBudgetReport_tb_cat_wise(DateTime frm, DateTime to, DateTime frmpy, DateTime topy, string channel, string pc, string cate, string com, string user)
        {
            try
            {
                List<GLB_PROFITABILITY> det = new List<GLB_PROFITABILITY>();

                OracleParameter[] param = new OracleParameter[10];
                (param[0] = new OracleParameter("P_FRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = frm;
                (param[1] = new OracleParameter("P_TODT", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
                (param[2] = new OracleParameter("P_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
                (param[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[4] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cate;
                (param[5] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[6] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
                (param[7] = new OracleParameter("P_PYFRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmpy;
                (param[8] = new OracleParameter("P_PYTODT", OracleDbType.Date, null, ParameterDirection.Input)).Value = topy;
                param[9] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_BUDGETREPORT_CAT_WISE", CommandType.StoredProcedure, false, param);

                //if (_dtResults.Rows.Count > 0)
                //{
                //    det = DataTableExtensions.ToGenericList<GLB_PROFITABILITY>(_dtResults, GLB_PROFITABILITY.Converter1);
                //}
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INVENTORY_SHIPMENT> getInvShipDetails(Int32 pagenum, Int32 pagesize, string companylst, string runtp, DateTime frmdt, DateTime todt, string BrandMngr, string Brand, string MainCat, string txtModel, string txtItem)
        {
            try
            {
                List<INVENTORY_SHIPMENT> det = new List<INVENTORY_SHIPMENT>();

                OracleParameter[] param = new OracleParameter[12];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pagenum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pagesize;
                (param[2] = new OracleParameter("p_dtfilter", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = runtp;
                (param[3] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmdt;
                (param[4] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt;
                (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = companylst;
                (param[6] = new OracleParameter("p_brandmngr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BrandMngr;
                (param[7] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Brand;
                (param[8] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtModel;
                (param[9] = new OracleParameter("p_maincat", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = MainCat;
                (param[10] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = txtItem;
                param[11] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_INVENTORYSHIPMENTDET", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<INVENTORY_SHIPMENT>(_dtResults, INVENTORY_SHIPMENT.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INVENTORY_SHIPMENT_SI> loadBLItemDetails(int pagenum, int pagesize, string checkcom,string blno)
        {
            try
            {
                List<INVENTORY_SHIPMENT_SI> det = new List<INVENTORY_SHIPMENT_SI>();

                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pagenum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pagesize;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = checkcom;
                (param[3] = new OracleParameter("p_blno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = blno;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_GET_BLSTOCKBALDETBITOOL", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    det = DataTableExtensions.ToGenericList<INVENTORY_SHIPMENT_SI>(_dtResults, INVENTORY_SHIPMENT_SI.Converter);
                }
                return det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Added by Udesh 21-Jan-2019
        public DataTable GetPC_from_Hierachy_with_ShowroomMgr(string com, string channel, string subChannel, string area, string region, string zone, string pc_code, string opteam, string town, string district, string province, string userId)
        {

            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_cha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (param[2] = new OracleParameter("p_subcha", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subChannel;
            (param[3] = new OracleParameter("p_area", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = area;
            (param[4] = new OracleParameter("p_region", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = region;
            (param[5] = new OracleParameter("p_zone", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = zone;
            (param[6] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc_code;
            (param[7] = new OracleParameter("p_sr_mgr_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = opteam;
            (param[8] = new OracleParameter("p_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = town;
            (param[9] = new OracleParameter("p_district", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = district;
            (param[10] = new OracleParameter("p_province", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = province;
            (param[11] = new OracleParameter("userId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            param[12] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbldata", "SP_GET_PC_ONHIERACHY_SRMGR", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Added by Udesh 21-Jan-2019
        public int SaveCommaSeparatedValues(string commaValue, string valueType, string reportType, string userID)
        {
            ConnectionOpen_DR();
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = valueType;
            (param[1] = new OracleParameter("p_value", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = commaValue;
            (param[2] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reportType;
            (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_ADD_PARAMETERVLUES", CommandType.StoredProcedure, param);
            TransactionCommit();//Commit the transaction
            ConnectionClose();
            return effects;
        }


        //Added by Udesh 21-Jan-2019
        public int RemoveCommaSeparatedValues(string reportType, string userID)
        {
            ConnectionOpen_DR();
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_report", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reportType;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_DELETE_PARAMETERVLUES", CommandType.StoredProcedure, param);
            TransactionCommit();//Commit the transaction
            ConnectionClose();
            return effects;
        }

        //Added by Udesh 21-Jan-2019
        public List<BMT_REF_HEAD> getPcPerfomanceProperties(string _searchValue, string _pageNum, string _pageSize, string _serachField, string _propertyType)
        {
            try
            {
                switch (_serachField)
                {
                    case "Code":
                        _serachField = "BMR_COL_NM";
                        break;
                    case "Description":
                        _serachField = "BMR_COL_DESC";
                        break;
                    default:
                        _serachField = "";
                        break;

                }

                List<BMT_REF_HEAD> cuslist = null;
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageNum;
                (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pageSize;
                (param[2] = new OracleParameter("p_colDescription", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _searchValue;
                (param[3] = new OracleParameter("p_serachField", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serachField;
                (param[4] = new OracleParameter("p_propertyType", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _propertyType;
                param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _dtResults = QueryDataTable("tblRef", "SP_Srch_PC_Perfo_Properties", CommandType.StoredProcedure, false, param);

                if (_dtResults.Rows.Count > 0)
                {
                    cuslist = DataTableExtensions.ToGenericList<BMT_REF_HEAD>(_dtResults, BMT_REF_HEAD.Converter);
                }
                return cuslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Added by Udesh 21-Jan-2019
        public DataTable getPcPerformanceReport(DateTime fromDate, DateTime toDate, bool isMainProfitCenter, string userId)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_fromDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromDate;
            (param[1] = new OracleParameter("p_toDt", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            (param[2] = new OracleParameter("P_userId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            (param[3] = new OracleParameter("isMainProfitCenter", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isMainProfitCenter ? 1 : 0;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PC_PERF_RPT", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Added by Udesh 21-Jan-2019
        public DataTable getProfitCenterHeader(string column, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_column", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = column;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblcrg", "SP_GET_PROFIT_CENTER_HDR", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }



        public List<ITEM_BI_AGE> getItemAgeDetails(DateTime mnthend, string itemcode, string companylst)
        {
            try
            {
                List<ITEM_BI_AGE> dtl = new List<ITEM_BI_AGE>();
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_mnthend", OracleDbType.Date, null, ParameterDirection.Input)).Value = mnthend.Date;
                (param[1] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = companylst;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _result = QueryDataTable("tbl", "SP_GET_ITEMAGEFORBI", CommandType.StoredProcedure, false, param);
                if (_result.Rows.Count > 0)
                {
                    dtl = DataTableExtensions.ToGenericList<ITEM_BI_AGE>(_result, ITEM_BI_AGE.Converter);
                }
                return dtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SI_BAL_DET> getSiItemBalDet(string itemcode, string companylst)
        {
            try
            {
                List<SI_BAL_DET> dtl = new List<SI_BAL_DET>();
                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = companylst;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _result = QueryDataTable("tbl", "SP_GET_BLITEMPENDINGDET", CommandType.StoredProcedure, false, param);
                if (_result.Rows.Count > 0)
                {
                    dtl = DataTableExtensions.ToGenericList<SI_BAL_DET>(_result, SI_BAL_DET.Converter);
                }
                return dtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ITEM_SALE_SUMM> getBiSalesItemInv(string checkcom, string itemcode, DateTime fromdt, DateTime todt)
        {
            try
            {
                List<ITEM_SALE_SUMM> dtl = new List<ITEM_SALE_SUMM>();
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemcode;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = checkcom;
                (param[2] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdt;
                (param[3] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = todt;
                param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                DataTable _result = QueryDataTable("tbl", "SP_GETINVMANAGEMENTPRESALES", CommandType.StoredProcedure, false, param);
                if (_result.Rows.Count > 0)
                {
                    dtl = DataTableExtensions.ToGenericList<ITEM_SALE_SUMM>(_result, ITEM_SALE_SUMM.Converter);
                }
                return dtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
