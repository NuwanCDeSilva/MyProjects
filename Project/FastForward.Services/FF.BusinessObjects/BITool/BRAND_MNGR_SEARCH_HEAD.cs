using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BRAND_MNGR_SEARCH_HEAD
    {
        public string mba_emp_id{get;set;}
        public string  esep_com_cd{get;set;}
        public string esep_epf{get;set;}
        public string esep_first_name{get;set;}
        public string esep_manager_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static BRAND_MNGR_SEARCH_HEAD Converter(DataRow row)
        {
            return new BRAND_MNGR_SEARCH_HEAD
            {
                mba_emp_id = row["mba_emp_id"] == DBNull.Value ? string.Empty : row["mba_emp_id"].ToString(),
                esep_com_cd = row["esep_com_cd"] == DBNull.Value ? string.Empty : row["esep_com_cd"].ToString(),
                esep_epf = row["esep_epf"] == DBNull.Value ? string.Empty : row["esep_epf"].ToString(),
                esep_first_name = row["esep_first_name"] == DBNull.Value ? string.Empty : row["esep_first_name"].ToString(),
                esep_manager_cd = row["esep_manager_cd"] == DBNull.Value ? string.Empty : row["esep_manager_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
