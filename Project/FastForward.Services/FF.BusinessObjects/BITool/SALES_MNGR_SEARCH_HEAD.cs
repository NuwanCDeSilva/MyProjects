using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class SALES_MNGR_SEARCH_HEAD
    {
        public string esep_com_cd { get; set; }
        public string esep_epf { get; set; }
        public string esep_first_name { get; set; }
        public string esep_manager_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static SALES_MNGR_SEARCH_HEAD Converter(DataRow row)
        {
            return new SALES_MNGR_SEARCH_HEAD
            {
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
