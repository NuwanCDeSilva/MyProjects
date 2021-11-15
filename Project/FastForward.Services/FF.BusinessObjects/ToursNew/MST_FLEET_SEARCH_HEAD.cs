using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class mst_fleet_search_head
    {
       
        public String MSTF_REGNO { get; set; }
        public String MSTF_VEH_TP { get; set; }
        public String MSTF_MODEL { get; set; }
        public String MSTF_OWN_NM { get; set; }
        public string MSTF_OWN_CONT { get; set; }
        public String MSTF_BRD { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static mst_fleet_search_head Converter(DataRow row)
        {
            return new mst_fleet_search_head
            {
              
                MSTF_REGNO = row["MSTF_REGNO"] == DBNull.Value ? string.Empty : row["MSTF_REGNO"].ToString(),
                MSTF_VEH_TP = row["MSTF_VEH_TP"] == DBNull.Value ? string.Empty : row["MSTF_VEH_TP"].ToString(),
                MSTF_MODEL = row["MSTF_MODEL"] == DBNull.Value ? string.Empty : row["MSTF_MODEL"].ToString(),
                MSTF_OWN_NM = row["MSTF_OWN_NM"] == DBNull.Value ? string.Empty : row["MSTF_OWN_NM"].ToString(),
                MSTF_OWN_CONT = row["MSTF_OWN_CONT"] == DBNull.Value ? string.Empty : row["MSTF_OWN_CONT"].ToString(),
                MSTF_BRD = row["MSTF_BRD"] == DBNull.Value ? string.Empty : row["MSTF_BRD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        } 
    }
}
