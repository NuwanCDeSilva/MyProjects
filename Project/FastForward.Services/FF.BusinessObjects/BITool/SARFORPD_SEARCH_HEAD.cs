using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SARFORPD_SEARCH_HEAD
    {
        public string sfp_pd_cd { get; set; }
        public string sfp_frm_pd { get; set; }
        public string sfp_to_pd { get; set; }
        public string sfp_desc { get; set; }
        public string sfp_cal_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static SARFORPD_SEARCH_HEAD Converter(DataRow row)
        {
            return new SARFORPD_SEARCH_HEAD
            {
                sfp_pd_cd = row["sfp_pd_cd"] == DBNull.Value ? string.Empty : row["sfp_pd_cd"].ToString(),
                sfp_frm_pd = row["sfp_frm_pd"] == DBNull.Value ? string.Empty :Convert.ToDateTime(row["sfp_frm_pd"].ToString()).ToString("d"),
                sfp_to_pd = row["sfp_to_pd"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["sfp_to_pd"].ToString()).ToString("d"),
                sfp_desc = row["sfp_desc"] == DBNull.Value ? string.Empty : row["sfp_desc"].ToString(),
                sfp_cal_cd = row["sfp_cal_cd"] == DBNull.Value ? string.Empty : row["sfp_cal_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
