using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalRemarkLog
    {
        public Int32 GRCL_LVL { get; set; }
        public string GRCL_DEPT { get; set; }
        public string GRCL_RMK { get; set; }
        public Int32 GRCL_APP_LVL { get; set; }
        public string GRCL_APP_STUS { get; set; }
        public string GRCL_REF { get; set; }
        public Int32 GRCL_REVISION { get; set; }

        public static ApprovalRemarkLog Converter(DataRow row)
        {
            return new ApprovalRemarkLog
            {
                GRCL_LVL = row["GRCL_LVL"] == DBNull.Value ? 0 :Convert.ToInt32(row["GRCL_LVL"].ToString()),
                GRCL_DEPT = row["GRCL_DEPT"] == DBNull.Value ? string.Empty : row["GRCL_DEPT"].ToString(),
                GRCL_RMK = row["GRCL_RMK"] == DBNull.Value ? string.Empty : row["GRCL_RMK"].ToString(),
                GRCL_APP_LVL = row["GRCL_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRCL_APP_LVL"].ToString()),
                GRCL_APP_STUS = row["GRCL_APP_STUS"] == DBNull.Value ? string.Empty : row["GRCL_APP_STUS"].ToString(),
                GRCL_REF = row["GRCL_REF"] == DBNull.Value ? string.Empty : row["GRCL_REF"].ToString(),
                GRCL_REVISION = row["GRCL_REVISION"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRCL_REVISION"].ToString())
            };
        }
    }
}
