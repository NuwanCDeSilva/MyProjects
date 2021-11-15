using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class SCMServiceStageLog
    {
        #region Private Members
        private string _sjl_createby;
        private DateTime _sjl_createwhen;
        private Boolean _sjl_infsup;
        private string _sjl_jobno;
        private decimal _sjl_jobstage;
        private string _sjl_location;
        private string _sjl_otherdocno;
        private string _sjl_reqno;
        private Int32 _sjl_seqno;
        #endregion

        public string Sjl_createby
        {
            get { return _sjl_createby; }
            set { _sjl_createby = value; }
        }
        public DateTime Sjl_createwhen
        {
            get { return _sjl_createwhen; }
            set { _sjl_createwhen = value; }
        }
        public Boolean Sjl_infsup
        {
            get { return _sjl_infsup; }
            set { _sjl_infsup = value; }
        }
        public string Sjl_jobno
        {
            get { return _sjl_jobno; }
            set { _sjl_jobno = value; }
        }
        public decimal Sjl_jobstage
        {
            get { return _sjl_jobstage; }
            set { _sjl_jobstage = value; }
        }
        public string Sjl_location
        {
            get { return _sjl_location; }
            set { _sjl_location = value; }
        }
        public string Sjl_otherdocno
        {
            get { return _sjl_otherdocno; }
            set { _sjl_otherdocno = value; }
        }
        public string Sjl_reqno
        {
            get { return _sjl_reqno; }
            set { _sjl_reqno = value; }
        }
        public Int32 Sjl_seqno
        {
            get { return _sjl_seqno; }
            set { _sjl_seqno = value; }
        }

        public static SCMServiceStageLog Converter(DataRow row)
        {
            return new SCMServiceStageLog
            {
                Sjl_createby = row["SJL_CREATEBY"] == DBNull.Value ? string.Empty : row["SJL_CREATEBY"].ToString(),
                Sjl_createwhen = row["SJL_CREATEWHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SJL_CREATEWHEN"]),
                Sjl_infsup = row["SJL_INFSUP"] == DBNull.Value ? false : Convert.ToBoolean(row["SJL_INFSUP"]),
                Sjl_jobno = row["SJL_JOBNO"] == DBNull.Value ? string.Empty : row["SJL_JOBNO"].ToString(),
                Sjl_jobstage = row["SJL_JOBSTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SJL_JOBSTAGE"]),
                Sjl_location = row["SJL_LOCATION"] == DBNull.Value ? string.Empty : row["SJL_LOCATION"].ToString(),
                Sjl_otherdocno = row["SJL_OTHERDOCNO"] == DBNull.Value ? string.Empty : row["SJL_OTHERDOCNO"].ToString(),
                Sjl_reqno = row["SJL_REQNO"] == DBNull.Value ? string.Empty : row["SJL_REQNO"].ToString(),
                Sjl_seqno = row["SJL_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SJL_SEQNO"])

            };
        }

    }
}
