using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 27-Jan-2015 05:44:12
    //===========================================================================================================

    public class scv_prit_task
    {
        public String Spit_com { get; set; }
        public String Spit_pty_tp { get; set; }
        public String Spit_pty_cd { get; set; }
        public String Spit_prit_cd { get; set; }
        public Decimal Spit_stage { get; set; }
        public Decimal Spit_expt_dur { get; set; }
        public String Spit_expt_tp { get; set; }
        public Int32 Spit_alrt_seq { get; set; }
        public String Spit_cre_by { get; set; }
        public DateTime Spit_cre_when { get; set; }

        public String Spit_mod_by { get; set; }
        public DateTime Spit_mod_when { get; set; }

        public String JBS_DESC { get; set; }
        public String Spit_expt_durTxt { get; set; }

        public static scv_prit_task Converter(DataRow row)
        {
            return new scv_prit_task
            {
                Spit_com = row["SPIT_COM"] == DBNull.Value ? string.Empty : row["SPIT_COM"].ToString(),
                Spit_pty_tp = row["SPIT_PTY_TP"] == DBNull.Value ? string.Empty : row["SPIT_PTY_TP"].ToString(),
                Spit_pty_cd = row["SPIT_PTY_CD"] == DBNull.Value ? string.Empty : row["SPIT_PTY_CD"].ToString(),
                Spit_prit_cd = row["SPIT_PRIT_CD"] == DBNull.Value ? string.Empty : row["SPIT_PRIT_CD"].ToString(),
                Spit_stage = row["SPIT_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPIT_STAGE"].ToString()),
                Spit_expt_dur = row["SPIT_EXPT_DUR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPIT_EXPT_DUR"].ToString()),
                Spit_expt_tp = row["SPIT_EXPT_TP"] == DBNull.Value ? string.Empty : row["SPIT_EXPT_TP"].ToString(),
                Spit_alrt_seq = row["SPIT_ALRT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPIT_ALRT_SEQ"].ToString()),
                Spit_cre_by = row["Spit_cre_by"] == DBNull.Value ? string.Empty : row["Spit_cre_by"].ToString(),
                Spit_cre_when = row["Spit_cre_when"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["Spit_cre_when"]),
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString()
            };
        }

        public static scv_prit_task ConverterPure(DataRow row)
        {
            return new scv_prit_task
            {
                Spit_com = row["SPIT_COM"] == DBNull.Value ? string.Empty : row["SPIT_COM"].ToString(),
                Spit_pty_tp = row["SPIT_PTY_TP"] == DBNull.Value ? string.Empty : row["SPIT_PTY_TP"].ToString(),
                Spit_pty_cd = row["SPIT_PTY_CD"] == DBNull.Value ? string.Empty : row["SPIT_PTY_CD"].ToString(),
                Spit_prit_cd = row["SPIT_PRIT_CD"] == DBNull.Value ? string.Empty : row["SPIT_PRIT_CD"].ToString(),
                Spit_stage = row["SPIT_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPIT_STAGE"].ToString()),
                Spit_expt_dur = row["SPIT_EXPT_DUR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPIT_EXPT_DUR"].ToString()),
                Spit_expt_tp = row["SPIT_EXPT_TP"] == DBNull.Value ? string.Empty : row["SPIT_EXPT_TP"].ToString(),
                Spit_alrt_seq = row["SPIT_ALRT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPIT_ALRT_SEQ"].ToString()),
                Spit_cre_by = row["SPIT_CRE_BY"] == DBNull.Value ? string.Empty : row["SPIT_CRE_BY"].ToString(),
                Spit_cre_when = row["SPIT_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPIT_CRE_WHEN"].ToString()),
                Spit_mod_by = row["SPIT_MOD_BY"] == DBNull.Value ? string.Empty : row["SPIT_MOD_BY"].ToString(),
                Spit_mod_when = row["SPIT_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPIT_MOD_WHEN"].ToString())
            };
        }

    }
}
