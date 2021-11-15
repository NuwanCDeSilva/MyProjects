using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 27-Jan-2015 11:55:12
    //===========================================================================================================

    public class MST_BUSPRIT_TASK
    {
        public String Mbt_cd { get; set; }
        public String Mbt_com { get; set; }
        public String Mbt_pty_tp { get; set; }
        public String Mbt_pty_cd { get; set; }
        public String Mbt_prit_cd { get; set; }
        public Decimal Mbt_stage { get; set; }
        public Decimal Mbt_expt_dur { get; set; }
        public String Mbt_expt_tp { get; set; }
        public Int32 Mbt_alrt_seq { get; set; }

        public String Mbt_expt_durText { get; set; }
        public String JBS_DESC { get; set; }

        public static MST_BUSPRIT_TASK Converter(DataRow row)
        {
            return new MST_BUSPRIT_TASK
            {
                Mbt_cd = row["MBT_CD"] == DBNull.Value ? string.Empty : row["MBT_CD"].ToString(),
                Mbt_com = row["MBT_COM"] == DBNull.Value ? string.Empty : row["MBT_COM"].ToString(),
                Mbt_pty_tp = row["MBT_PTY_TP"] == DBNull.Value ? string.Empty : row["MBT_PTY_TP"].ToString(),
                Mbt_pty_cd = row["MBT_PTY_CD"] == DBNull.Value ? string.Empty : row["MBT_PTY_CD"].ToString(),
                Mbt_prit_cd = row["MBT_PRIT_CD"] == DBNull.Value ? string.Empty : row["MBT_PRIT_CD"].ToString(),
                Mbt_stage = row["MBT_STAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBT_STAGE"].ToString()),
                Mbt_expt_dur = row["MBT_EXPT_DUR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBT_EXPT_DUR"].ToString()),
                Mbt_expt_tp = row["MBT_EXPT_TP"] == DBNull.Value ? string.Empty : row["MBT_EXPT_TP"].ToString(),
                Mbt_alrt_seq = row["MBT_ALRT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBT_ALRT_SEQ"].ToString()),
                JBS_DESC = row["JBS_DESC"] == DBNull.Value ? string.Empty : row["JBS_DESC"].ToString()
            };
        }
    }
}
