using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalReqCategory
    {
        public string MMCT_SCD{get;set;}
        public string MMCT_SDESC { get; set; }
          public Int32 MMCT_ACT { get; set; }
          public Int32 MMCT_ALW_REQ { get; set; }
          public Int32 MMCT_ADJ_PLUS { get; set; }
          public Int32 MMCT_ADJ_MIN { get; set; }
          public Int32 MMCT_STUS_CHNG { get; set; }
        public static ApprovalReqCategory Converter(DataRow row)
        {
            return new ApprovalReqCategory
            {
                MMCT_SCD = row["MMCT_SCD"] == DBNull.Value ? string.Empty : row["MMCT_SCD"].ToString(),
                MMCT_SDESC = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString()
            };
        }
          public static ApprovalReqCategory Converternew(DataRow row)
        {
            return new ApprovalReqCategory
            {
                MMCT_SCD = row["MMCT_SCD"] == DBNull.Value ? string.Empty : row["MMCT_SCD"].ToString(),
                MMCT_SDESC = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString(),
                MMCT_ACT= row["MMCT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMCT_ACT"]),
                MMCT_ALW_REQ = row["MMCT_ALW_REQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMCT_ALW_REQ"]),
                MMCT_ADJ_PLUS=row["MMCT_ADJ_PLUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMCT_ADJ_PLUS"]),
                MMCT_ADJ_MIN=row["MMCT_ADJ_MIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMCT_ADJ_MIN"])
            };
        }
    }
}
