using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class hpr_sch_det
    {
        public String hsd_cd { get; set; }
        public String hsd_desc { get; set; }
        public Int32 hsd_term { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static hpr_sch_det webConverter(DataRow row)
        {
            return new hpr_sch_det
            {
                hsd_cd = row["hsd_cd"] == DBNull.Value ? string.Empty : row["hsd_cd"].ToString(),
                hsd_desc = row["hsd_desc"] == DBNull.Value ? string.Empty : row["hsd_desc"].ToString(),
                hsd_term = row["hsd_term"] == DBNull.Value ? 0 : Convert.ToInt32(row["hsd_term"]),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
