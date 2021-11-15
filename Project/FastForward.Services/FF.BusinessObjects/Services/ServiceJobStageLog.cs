using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ServiceJobStageLog
    {
        /// <summary>
        /// Written By Shani on 21/08/2012
        /// Table: sev_job_stagelog
        /// </summary>
        #region Private Members
        private string _sjl_cre_by;
        private DateTime _sjl_cre_dt;
        private string _sjl_jobno;
        private decimal _sjl_jobstage;
        private string _sjl_loc;
        private string _sjl_othdocno;
        private string _sjl_reqno;
        private Int32 _sjl_seqno;
        #endregion

        public string Sjl_cre_by { get { return _sjl_cre_by; } set { _sjl_cre_by = value; } }
        public DateTime Sjl_cre_dt { get { return _sjl_cre_dt; } set { _sjl_cre_dt = value; } }
        public string Sjl_jobno { get { return _sjl_jobno; } set { _sjl_jobno = value; } }
        public decimal Sjl_jobstage { get { return _sjl_jobstage; } set { _sjl_jobstage = value; } }
        public string Sjl_loc { get { return _sjl_loc; } set { _sjl_loc = value; } }
        public string Sjl_othdocno { get { return _sjl_othdocno; } set { _sjl_othdocno = value; } }
        public string Sjl_reqno { get { return _sjl_reqno; } set { _sjl_reqno = value; } }
        public Int32 Sjl_seqno { get { return _sjl_seqno; } set { _sjl_seqno = value; } }

        public static ServiceJobStageLog Converter(DataRow row)
        {
            return new ServiceJobStageLog
            {
                Sjl_cre_by = row["SJL_CRE_BY"] == DBNull.Value ? string.Empty : row["SJL_CRE_BY"].ToString(),
                Sjl_cre_dt = row["SJL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJL_CRE_DT"]),
                Sjl_jobno = row["SJL_JOBNO"] == DBNull.Value ? string.Empty : row["SJL_JOBNO"].ToString(),
                Sjl_jobstage = row["SJL_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJL_JOBSTAGE"]),
                Sjl_loc = row["SJL_LOC"] == DBNull.Value ? string.Empty : row["SJL_LOC"].ToString(),
                Sjl_othdocno = row["SJL_OTHDOCNO"] == DBNull.Value ? string.Empty : row["SJL_OTHDOCNO"].ToString(),
                Sjl_reqno = row["SJL_REQNO"] == DBNull.Value ? string.Empty : row["SJL_REQNO"].ToString(),
                Sjl_seqno = row["SJL_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_SEQNO"])

            };
        }
    }
}
