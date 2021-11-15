using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class AuditRemarkValue
    {
        #region Private Members
        private string _ausv_cre_by;
        private DateTime _ausv_cre_dt;
        private Int32 _ausv_id;
        private string _ausv_itm;
        private string _ausv_itm_stus;
        private string _ausv_job;
        private Int32 _ausv_job_seq;
        private int _ausv_line;
        private string _ausv_rpt_cd;
        private string _ausv_val;
        private int _ausv_ser_id;
        #endregion

        public string Ausv_cre_by
        {
            get { return _ausv_cre_by; }
            set { _ausv_cre_by = value; }
        }
        public DateTime Ausv_cre_dt
        {
            get { return _ausv_cre_dt; }
            set { _ausv_cre_dt = value; }
        }
        public Int32 Ausv_id
        {
            get { return _ausv_id; }
            set { _ausv_id = value; }
        }
        public string Ausv_itm
        {
            get { return _ausv_itm; }
            set { _ausv_itm = value; }
        }
        public string Ausv_itm_stus
        {
            get { return _ausv_itm_stus; }
            set { _ausv_itm_stus = value; }
        }
        public string Ausv_job
        {
            get { return _ausv_job; }
            set { _ausv_job = value; }
        }
        public Int32 Ausv_job_seq
        {
            get { return _ausv_job_seq; }
            set { _ausv_job_seq = value; }
        }
        public int Ausv_line
        {
            get { return _ausv_line; }
            set { _ausv_line = value; }
        }
        public string Ausv_rpt_cd
        {
            get { return _ausv_rpt_cd; }
            set { _ausv_rpt_cd = value; }
        }
        public string Ausv_val
        {
            get { return _ausv_val; }
            set { _ausv_val = value; }
        }
        public int Ausv_ser_id
        {
            get { return _ausv_ser_id; }
            set { _ausv_ser_id = value; }
        }

        public string Ausv_rpt_stus { get; set; }

       //Akila 2017/04/28
        public string Ausv_Mod_By { get; set; }
        public DateTime? Ausv_Mod_date { get; set; }
        public string Ausv_Session_Id { get; set; }

        public static AuditRemarkValue Converter(DataRow row)
        {
            return new AuditRemarkValue
            {
                Ausv_cre_by = row["AUSV_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSV_CRE_BY"].ToString(),
                Ausv_cre_dt = row["AUSV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSV_CRE_DT"]),
                Ausv_id = row["AUSV_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSV_ID"]),
                Ausv_itm = row["AUSV_ITM"] == DBNull.Value ? string.Empty : row["AUSV_ITM"].ToString(),
                Ausv_itm_stus = row["AUSV_ITM_STUS"] == DBNull.Value ? string.Empty : row["AUSV_ITM_STUS"].ToString(),
                Ausv_job = row["AUSV_JOB"] == DBNull.Value ? string.Empty : row["AUSV_JOB"].ToString(),
                Ausv_job_seq = row["AUSV_JOB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSV_JOB_SEQ"]),
                Ausv_line = row["AUSV_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["AUSV_LINE"]),
                Ausv_rpt_cd = row["AUSV_RPT_CD"] == DBNull.Value ? string.Empty : row["AUSV_RPT_CD"].ToString(),
                Ausv_val = row["AUSV_VAL"] == DBNull.Value ? string.Empty : row["AUSV_VAL"].ToString(),
                Ausv_ser_id = row["ausv_ser_id"] == DBNull.Value ? 0 : Convert.ToInt32(row["ausv_ser_id"].ToString()),
                Ausv_rpt_stus = row["ausv_rpt_stus"] == DBNull.Value ? string.Empty : row["ausv_rpt_stus"].ToString(),
            };
        }


    }
}
