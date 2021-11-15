using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{//Dulaj 2018/Nov/28
    public class MobilePrefix
    {
        public Int32 MR_SEQ { get; set; }
        public string MR_PRO_CD { get; set; }
        public Int32 MR_PREF { get; set; }
        public string MR_CRE_BY { get; set; }
        public DateTime MR_CRE_DT { get; set; }
        public string MR_MOD_BY { get; set; }
        public DateTime MR_MOD_DT { get; set; }
        public string MR_SESSION { get; set; }

        public static MobilePrefix Converter(DataRow row)
        {
            return new MobilePrefix
            {
                MR_SEQ = row["MR_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MR_SEQ"]),
                MR_PRO_CD = row["MR_PRO_CD"] == DBNull.Value ? string.Empty : row["MR_PRO_CD"].ToString(),
                MR_PREF = row["MR_PREF"] == DBNull.Value ? 0 : Convert.ToInt32(row["MR_PREF"]),
                MR_CRE_BY = row["MR_CRE_BY"] == DBNull.Value ? string.Empty : row["MR_CRE_BY"].ToString(),
                MR_CRE_DT = row["MR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MR_CRE_DT"]),
                MR_MOD_BY = row["MR_MOD_BY"] == DBNull.Value ? string.Empty : row["MR_MOD_BY"].ToString(),
                MR_MOD_DT = row["MR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MR_MOD_DT"]),
                MR_SESSION = row["MR_SESSION"] == DBNull.Value ? string.Empty : row["MR_SESSION"].ToString()           
               
            };
        }
    }
}
