using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 03-Oct-2014 02:37:04
    //===========================================================================================================

    public class Service_Job_StageLog
    {
        public Int32 SJL_SEQNO { get; set; }

        public String SJL_REQNO { get; set; }

        public Int32 SJL_REQLINE { get; set; }

        public String SJL_JOBNO { get; set; }

        public Int32 SJL_JOBLINE { get; set; }

        public String SJL_COM { get; set; }

        public String SJL_LOC { get; set; }

        public String SJL_OTHERDOCNO { get; set; }

        public Decimal SJL_JOBSTAGE { get; set; }

        public String SJL_CRE_BY { get; set; }

        public DateTime SJL_CRE_DT { get; set; }

        public String SJL_SESSION_ID { get; set; }

        public Int32 SJL_INFSUP { get; set; }

        public static Service_Job_StageLog Converter(DataRow row)
        {
            return new Service_Job_StageLog
            {
                SJL_SEQNO = row["SJL_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_SEQNO"].ToString()),
                SJL_REQNO = row["SJL_REQNO"] == DBNull.Value ? string.Empty : row["SJL_REQNO"].ToString(),
                SJL_REQLINE = row["SJL_REQLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_REQLINE"].ToString()),
                SJL_JOBNO = row["SJL_JOBNO"] == DBNull.Value ? string.Empty : row["SJL_JOBNO"].ToString(),
                SJL_JOBLINE = row["SJL_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_JOBLINE"].ToString()),
                SJL_COM = row["SJL_COM"] == DBNull.Value ? string.Empty : row["SJL_COM"].ToString(),
                SJL_LOC = row["SJL_LOC"] == DBNull.Value ? string.Empty : row["SJL_LOC"].ToString(),
                SJL_OTHERDOCNO = row["SJL_OTHERDOCNO"] == DBNull.Value ? string.Empty : row["SJL_OTHERDOCNO"].ToString(),
                SJL_JOBSTAGE = row["SJL_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJL_JOBSTAGE"].ToString()),
                SJL_CRE_BY = row["SJL_CRE_BY"] == DBNull.Value ? string.Empty : row["SJL_CRE_BY"].ToString(),
                SJL_CRE_DT = row["SJL_CRE_DT"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(row["SJL_CRE_DT"].ToString()),
                SJL_SESSION_ID = row["SJL_SESSION_ID"] == DBNull.Value ? string.Empty : row["SJL_SESSION_ID"].ToString(),
                SJL_INFSUP = row["SJL_INFSUP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_INFSUP"].ToString())
            };
        }
    }
}