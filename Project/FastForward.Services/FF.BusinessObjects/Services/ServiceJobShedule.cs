using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
      [Serializable]
    public class ServiceJobShedule
    {
        /// <summary>
        /// Written By Shani on 16/01/2013
        /// Table: sev_job_shed
        /// </summary>
        #region Private Members
        private string _svjs_cre_by;
        private DateTime _svjs_cre_dt;
        private string _svjs_inv_no;
        private string _svjs_itm;
        private string _svjs_job_no;
        private Int32 _svjs_seq;
        private string _svjs_ser_id;
        private DateTime _svjs_shed_dt;
        private Boolean _svjs_stus;
        private int _svjs_term;
        #endregion

        public string Svjs_cre_by { get { return _svjs_cre_by; } set { _svjs_cre_by = value; } }
        public DateTime Svjs_cre_dt { get { return _svjs_cre_dt; } set { _svjs_cre_dt = value; } }
        public string Svjs_inv_no { get { return _svjs_inv_no; } set { _svjs_inv_no = value; } }
        public string Svjs_itm { get { return _svjs_itm; } set { _svjs_itm = value; } }
        public string Svjs_job_no { get { return _svjs_job_no; } set { _svjs_job_no = value; } }
        public Int32 Svjs_seq { get { return _svjs_seq; } set { _svjs_seq = value; } }
        public string Svjs_ser_id { get { return _svjs_ser_id; } set { _svjs_ser_id = value; } }
        public DateTime Svjs_shed_dt { get { return _svjs_shed_dt; } set { _svjs_shed_dt = value; } }
        public Boolean Svjs_stus { get { return _svjs_stus; } set { _svjs_stus = value; } }
        public int Svjs_term { get { return _svjs_term; } set { _svjs_term = value; } }

        public static ServiceJobShedule Converter(DataRow row)
        {
            return new ServiceJobShedule
            {
                Svjs_cre_by = row["SVJS_CRE_BY"] == DBNull.Value ? string.Empty : row["SVJS_CRE_BY"].ToString(),
                Svjs_cre_dt = row["SVJS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVJS_CRE_DT"]),
                Svjs_inv_no = row["SVJS_INV_NO"] == DBNull.Value ? string.Empty : row["SVJS_INV_NO"].ToString(),
                Svjs_itm = row["SVJS_ITM"] == DBNull.Value ? string.Empty : row["SVJS_ITM"].ToString(),
                Svjs_job_no = row["SVJS_JOB_NO"] == DBNull.Value ? string.Empty : row["SVJS_JOB_NO"].ToString(),
                Svjs_seq = row["SVJS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVJS_SEQ"]),
                //Svjs_ser_id = row["SVJS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVJS_SER_ID"]),
                Svjs_ser_id = row["SVJS_SER_ID"] == DBNull.Value ? string.Empty : row["SVJS_SER_ID"].ToString(),
                Svjs_shed_dt = row["SVJS_SHED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVJS_SHED_DT"]),
                Svjs_stus = row["SVJS_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SVJS_STUS"]),
                Svjs_term = row["SVJS_TERM"] == DBNull.Value ? 0 : Convert.ToInt16(row["SVJS_TERM"])

            };
        }

    }
}
