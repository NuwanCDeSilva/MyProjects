using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class TransportParty
    {
        public Int32 Rtnp_seq { get; set; }
        public Int32 Rtnp_tnpt_seq { get; set; }
        public String Rtnp_pty_tp { get; set; }
        public String Rtnp_pty_cd { get; set; }
        public Int32 Rtnp_act { get; set; }
        public Int32 Rtnp_is_flt_job { get; set; }
        public string Mbe_name { get; set; }
        public string Mbe_com { get; set; }
        public static TransportParty Converter(DataRow row)
        {
            return new TransportParty
            {
                Rtnp_seq = row["RTNP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_SEQ"].ToString()),
                Rtnp_tnpt_seq = row["RTNP_TNPT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_TNPT_SEQ"].ToString()),
                Rtnp_pty_tp = row["RTNP_PTY_TP"] == DBNull.Value ? string.Empty : row["RTNP_PTY_TP"].ToString(),
                Rtnp_pty_cd = row["RTNP_PTY_CD"] == DBNull.Value ? string.Empty : row["RTNP_PTY_CD"].ToString(),
                Rtnp_act = row["RTNP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_ACT"].ToString()),
                Rtnp_is_flt_job = row["RTNP_IS_FLT_JOB"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_IS_FLT_JOB"].ToString())
            };
        }

        public static TransportParty ConverterNew(DataRow row)
        {
            return new TransportParty
            {
                Rtnp_seq = row["RTNP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_SEQ"].ToString()),
                Rtnp_tnpt_seq = row["RTNP_TNPT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_TNPT_SEQ"].ToString()),
                Rtnp_pty_tp = row["RTNP_PTY_TP"] == DBNull.Value ? string.Empty : row["RTNP_PTY_TP"].ToString(),
                Rtnp_pty_cd = row["RTNP_PTY_CD"] == DBNull.Value ? string.Empty : row["RTNP_PTY_CD"].ToString(),
                Rtnp_act = row["RTNP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_ACT"].ToString()),
                Rtnp_is_flt_job = row["RTNP_IS_FLT_JOB"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTNP_IS_FLT_JOB"].ToString()),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_com = row["mbe_com"] == DBNull.Value ? string.Empty : row["mbe_com"].ToString()
            };
        }
    }
}
