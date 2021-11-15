using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_DIST_SEARCH
    {
        public string mds_prov_cd { get; set; }
        public string mds_province { get; set; }
        public string mds_dist_cd { get; set; }
        public string mds_district { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_DIST_SEARCH Converter(DataRow row)
        {
            return new MST_DIST_SEARCH
            {
                mds_prov_cd = row["mds_province"] == DBNull.Value ? string.Empty : row["mds_province"].ToString(),
                mds_province = row["mds_province"] == DBNull.Value ? string.Empty : row["mds_province"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

        public static MST_DIST_SEARCH Converter2(DataRow row)
        {
            return new MST_DIST_SEARCH
            {
                mds_dist_cd = row["mds_dist_cd"] == DBNull.Value ? string.Empty : row["mds_dist_cd"].ToString(),
                mds_district = row["mds_district"] == DBNull.Value ? string.Empty : row["mds_district"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_PPROV_SELECTED
    {
        public string mds_prov_cd { get; set; }
        public string mds_province { get; set; }
    }

    public class MST_PDIST_SELECTED
    {
        public string mds_dist_cd { get; set; }
        public string mds_district { get; set; }
    }
}
