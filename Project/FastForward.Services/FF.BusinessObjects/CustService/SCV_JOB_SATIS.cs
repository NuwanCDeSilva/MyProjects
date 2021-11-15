using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class SCV_JOB_SATIS
    {
        private string _ssj_cre_by;
        private DateTime _ssj_cre_dt;
        private Int32 _ssj_jobline;
        private string _ssj_jobno;
        private Int32 _ssj_quest_seq;
        private int _ssj_quest_val;
        private Int32 _ssj_seq_no;

        public string Ssj_cre_by
        {
            get { return _ssj_cre_by; }
            set { _ssj_cre_by = value; }
        }
        public DateTime Ssj_cre_dt
        {
            get { return _ssj_cre_dt; }
            set { _ssj_cre_dt = value; }
        }
        public Int32 Ssj_jobline
        {
            get { return _ssj_jobline; }
            set { _ssj_jobline = value; }
        }
        public string Ssj_jobno
        {
            get { return _ssj_jobno; }
            set { _ssj_jobno = value; }
        }
        public Int32 Ssj_quest_seq
        {
            get { return _ssj_quest_seq; }
            set { _ssj_quest_seq = value; }
        }
        public int Ssj_quest_val
        {
            get { return _ssj_quest_val; }
            set { _ssj_quest_val = value; }
        }
        public Int32 Ssj_seq_no
        {
            get { return _ssj_seq_no; }
            set { _ssj_seq_no = value; }
        }

        public static SCV_JOB_SATIS Converter(DataRow row)
        {
            return new SCV_JOB_SATIS
            {
                Ssj_cre_by = row["SSJ_CRE_BY"] == DBNull.Value ? string.Empty : row["SSJ_CRE_BY"].ToString(),
                Ssj_cre_dt = row["SSJ_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSJ_CRE_DT"]),
                Ssj_jobline = row["SSJ_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSJ_JOBLINE"]),
                Ssj_jobno = row["SSJ_JOBNO"] == DBNull.Value ? string.Empty : row["SSJ_JOBNO"].ToString(),
                Ssj_quest_seq = row["SSJ_QUEST_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSJ_QUEST_SEQ"]),
                Ssj_quest_val = row["SSJ_QUEST_VAL"] == DBNull.Value ? 0 : Convert.ToInt16(row["SSJ_QUEST_VAL"]),
                Ssj_seq_no = row["SSJ_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSJ_SEQ_NO"])
            };
        }
    }
}

