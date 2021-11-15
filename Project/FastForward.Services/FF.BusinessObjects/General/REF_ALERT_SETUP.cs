using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 03-Feb-2016 12:58:34
    //===========================================================================================================

    public class REF_ALERT_SETUP
    {
        public Int32 Rals_seq { get; set; }
        public String Rals_com { get; set; }
        public String Rals_pty_tp { get; set; }
        public String Rals_pty_cd { get; set; }
        public String Rals_mod_name { get; set; }
        public String Rals_mod_stus { get; set; }
        public String Rals_to_user { get; set; }
        public Int32 Rals_via_sms { get; set; }
        public Int32 Rals_via_email { get; set; }
        public Int32 Rals_act { get; set; }
        public static REF_ALERT_SETUP Converter(DataRow row)
        {
            return new REF_ALERT_SETUP
            {
                Rals_seq = row["RALS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RALS_SEQ"].ToString()),
                Rals_com = row["RALS_COM"] == DBNull.Value ? string.Empty : row["RALS_COM"].ToString(),
                Rals_pty_tp = row["RALS_PTY_TP"] == DBNull.Value ? string.Empty : row["RALS_PTY_TP"].ToString(),
                Rals_pty_cd = row["RALS_PTY_CD"] == DBNull.Value ? string.Empty : row["RALS_PTY_CD"].ToString(),
                Rals_mod_name = row["RALS_MOD_NAME"] == DBNull.Value ? string.Empty : row["RALS_MOD_NAME"].ToString(),
                Rals_mod_stus = row["RALS_MOD_STUS"] == DBNull.Value ? string.Empty : row["RALS_MOD_STUS"].ToString(),
                Rals_to_user = row["RALS_TO_USER"] == DBNull.Value ? string.Empty : row["RALS_TO_USER"].ToString(),
                Rals_via_sms = row["RALS_VIA_SMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RALS_VIA_SMS"].ToString()),
                Rals_via_email = row["RALS_VIA_EMAIL"] == DBNull.Value ? 0 : Convert.ToInt32(row["RALS_VIA_EMAIL"].ToString()),
                Rals_act = row["RALS_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RALS_ACT"].ToString())
            };
        }
    }
}
