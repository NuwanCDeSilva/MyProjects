using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FF.BusinessObjects.Search
{
    public class MST_DISTRICT_SEARCH
    {
        public string mdis_cd { get; set; }
        public string mdis_desc { get; set; }
        public string mpro_desc { get; set; }
        //public string mt_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_DISTRICT_SEARCH Converter(DataRow row)
        {
            return new MST_DISTRICT_SEARCH
            {
                mdis_cd = row["mdis_cd"] == DBNull.Value ? string.Empty : row["mdis_cd"].ToString(),
                mdis_desc = row["mdis_desc"] == DBNull.Value ? string.Empty : row["mdis_desc"].ToString(),
                mpro_desc = row["mpro_desc"] == DBNull.Value ? string.Empty : row["mpro_desc"].ToString(),
                //mt_cd = row["mt_cd"] == DBNull.Value ? string.Empty : row["mt_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class MST_DISTRICT_SELECTED
    {
        public string mdis_cd { get; set; }
        public string mdis_desc { get; set; }
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
