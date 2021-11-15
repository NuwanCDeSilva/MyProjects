using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ServiceJobDefect
    {
        /// <summary>
        /// Written By Shani on 21/08/2012
        /// Table: sev_job_defc 
        /// </summary>
        #region Private Members
        private Int32 _srd_defc_line;
        private string _srd_jobno;
        private string _srd_job_defc_rmk;
        private string _srd_job_defc_tp;
        private Int32 _srd_job_line;
        #endregion

        public Int32 Srd_defc_line { get { return _srd_defc_line; } set { _srd_defc_line = value; } }
        public string Srd_jobno { get { return _srd_jobno; } set { _srd_jobno = value; } }
        public string Srd_job_defc_rmk { get { return _srd_job_defc_rmk; } set { _srd_job_defc_rmk = value; } }
        public string Srd_job_defc_tp { get { return _srd_job_defc_tp; } set { _srd_job_defc_tp = value; } }
        public Int32 Srd_job_line { get { return _srd_job_line; } set { _srd_job_line = value; } }

        public static ServiceJobDefect Converter(DataRow row)
        {
            return new ServiceJobDefect
            {
                Srd_defc_line = row["SRD_DEFC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_DEFC_LINE"]),
                Srd_jobno = row["SRD_JOBNO"] == DBNull.Value ? string.Empty : row["SRD_JOBNO"].ToString(),
                Srd_job_defc_rmk = row["SRD_JOB_DEFC_RMK"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEFC_RMK"].ToString(),
                Srd_job_defc_tp = row["SRD_JOB_DEFC_TP"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEFC_TP"].ToString(),
                Srd_job_line = row["SRD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_JOB_LINE"])

            };
        }
    }
}
