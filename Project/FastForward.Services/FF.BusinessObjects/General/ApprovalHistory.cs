using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalHistory
    {
        public DateTime APP_DATE { get; set; }
        public DateTime GRAH_CRE_DT { get; set; }
        public string GRAH_REQ_REM { get; set; }
        public string GRAH_APP_STUS { get; set; }
        public Int32 GRAH_APP_LVL { get; set; }
        public string GRAH_REMAKS { get; set; }
        public Int32 GRAH_REF_LINE_NO { get; set; }
        public string SE_USR_NAME { get; set; }
        public string SE_USR_ID { get; set; }
        public string CATE_DESC { get; set; }
        public string APP_HED_TEXT { get; set; }
        public List<ApproveHistoryDetails> PPROVE_HISTORY_DET { get; set; }
        public List<ApprovalRemarkLog> APP_REMARK_LOG { get; set; }
        public static ApprovalHistory Converter(DataRow row)
        {
            return new ApprovalHistory
            {
                APP_DATE = row["APP_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["APP_DATE"].ToString()),
                GRAH_CRE_DT = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"].ToString()),
                GRAH_REQ_REM = row["GRAH_REQ_REM"] == DBNull.Value ? string.Empty : row["GRAH_REQ_REM"].ToString(),
                GRAH_APP_STUS = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
                GRAH_APP_LVL = row["GRAH_APP_LVL"] == DBNull.Value ? 0 :Convert.ToInt32(row["GRAH_APP_LVL"].ToString()),
                GRAH_REMAKS = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                GRAH_REF_LINE_NO = row["GRAH_REF_LINE_NO"] == DBNull.Value ? 0: Convert.ToInt32(row["GRAH_REF_LINE_NO"].ToString()),
                SE_USR_NAME = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString(),
                SE_USR_ID = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString()
            };
        }
    }
}
