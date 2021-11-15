using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PhsicalStockVerificationMain
    {
        #region Private Members
        private string _ausm_cre_by;
        private DateTime _ausm_cre_dt;
        private DateTime _ausm_dt;
        private string _ausm_job;
        private string _ausm_loc;
        private Int32 _ausm_seq;
        private Boolean _ausm_stus;
        private string _ausm_com;
        private string _ausm_main_job;

        #endregion

        public string Ausm_cre_by
        {
            get { return _ausm_cre_by; }
            set { _ausm_cre_by = value; }
        }
        public DateTime Ausm_cre_dt
        {
            get { return _ausm_cre_dt; }
            set { _ausm_cre_dt = value; }
        }
        public DateTime Ausm_dt
        {
            get { return _ausm_dt; }
            set { _ausm_dt = value; }
        }
        public string Ausm_job
        {
            get { return _ausm_job; }
            set { _ausm_job = value; }
        }
        public string Ausm_loc
        {
            get { return _ausm_loc; }
            set { _ausm_loc = value; }
        }
        public Int32 Ausm_seq
        {
            get { return _ausm_seq; }
            set { _ausm_seq = value; }
        }
        public Boolean Ausm_stus
        {
            get { return _ausm_stus; }
            set { _ausm_stus = value; }
        }
        public string Ausm_com
        {
            get { return _ausm_com; }
            set { _ausm_com = value; }
        }
        public string Ausm_main_job
        {
            get { return _ausm_main_job; }
            set { _ausm_main_job = value; }
        }

        //Add by Akila 2017/02/21
        public string Ausm_Subjob_Status { get; set; }
        public DateTime? Ausm_Subjob_Strdt { get; set; }
        public DateTime? Ausm_Subjob_Enddt { get; set; }

        //Akila 2017/04/28
        public string Ausm_Mod_By { get; set; }
        public DateTime? Ausm_Mod_Date { get; set; }
        public string Ausm_Session_Id { get; set; }

        public static PhsicalStockVerificationMain Converter(DataRow row)
        {
            return new PhsicalStockVerificationMain
            {
                Ausm_cre_by = row["AUSM_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSM_CRE_BY"].ToString(),
                Ausm_cre_dt = row["AUSM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSM_CRE_DT"]),
                Ausm_dt = row["AUSM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSM_DT"]),
                Ausm_job = row["AUSM_JOB"] == DBNull.Value ? string.Empty : row["AUSM_JOB"].ToString(),
                Ausm_loc = row["AUSM_LOC"] == DBNull.Value ? string.Empty : row["AUSM_LOC"].ToString(),
                Ausm_seq = row["AUSM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSM_SEQ"]),
                Ausm_stus = row["AUSM_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["AUSM_STUS"]),
                Ausm_com = row["AUSM_COM"] == DBNull.Value ? string.Empty : row["AUSM_COM"].ToString(),
                Ausm_main_job = row["ausm_main_job"] == DBNull.Value ? string.Empty : row["ausm_main_job"].ToString(),
                Ausm_Subjob_Status = row["ausm_subjob_sts"] == DBNull.Value ? string.Empty : row["ausm_subjob_sts"].ToString(),
                Ausm_Subjob_Strdt = row["ausm_subjob_strdt"] == null ? new DateTime?() : Convert.ToDateTime( row["ausm_subjob_strdt"]),
                Ausm_Subjob_Enddt = row["ausm_subjob_strdt"] == null ? new DateTime?() : Convert.ToDateTime(row["ausm_subjob_strdt"])
            };
        }

    }
}
