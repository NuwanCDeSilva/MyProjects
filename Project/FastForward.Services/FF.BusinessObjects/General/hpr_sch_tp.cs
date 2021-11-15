using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class hpr_sch_tp
    {
        public String hst_cd { get; set; }
        public String hst_sch_cat { get; set; }
        public String hst_desc { get; set; }
        public Int32 hst_def_intr { get; set; }
        public Int32 hst_act { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static hpr_sch_tp webConverter(DataRow row)
        {
            return new hpr_sch_tp
            {
                hst_cd = row["hst_cd"] == DBNull.Value ? string.Empty : row["hst_cd"].ToString(),
                //hst_sch_cat = row["hst_sch_cat"] == DBNull.Value ? string.Empty : row["hst_sch_cat"].ToString(),
                hst_desc = row["hst_desc"] == DBNull.Value ? string.Empty : row["hst_desc"].ToString(),
                //hst_def_intr = row["hst_def_intr"] == DBNull.Value ? 0 : Convert.ToInt32(row["hst_def_intr"].ToString()),
                //hst_act = row["hst_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["hst_act"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
