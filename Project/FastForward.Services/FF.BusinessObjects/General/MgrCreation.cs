using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class MgrCreation
    {
        public Int32 hmfa_seq { get; set; }
        public String hmfa_com { get; set; }
        public String hmfa_pc { get; set; }
        public String hmfa_mgr_cd { get; set; }
        public String hmfa_mgr_name { get; set; }
        public DateTime hmfa_acc_dt { get; set; }
        public DateTime hmfa_bonus_st_dt { get; set; }
        public DateTime hmfa_sr_open_dt { get; set; }
        public Boolean hmfa_act_stus { get; set; }
        public String hmfa_cre_by { get; set; }
        public DateTime hmfa_cre_dt { get; set; }
        public String hmfa_mod_by { get; set; }
        public DateTime hmfa_mod_dt { get; set; }
        public String hmfa_pc_cat { get; set; }
        public String hmfa_bonus_method { get; set; }
        public String hmfa_mainpc { get; set; }

        //public string RESULT_COUNT { get; set; }
        //public string R__ { get; set; }
        public static MgrCreation webConverter(DataRow row)
        {
            return new MgrCreation
            {
                hmfa_seq = row["HMFA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HMFA_SEQ"].ToString()),
                hmfa_com = row["HMFA_COM"] == DBNull.Value ? string.Empty : row["HMFA_COM"].ToString(),
                hmfa_pc = row["HMFA_PC"] == DBNull.Value ? string.Empty : row["HMFA_PC"].ToString(),
                hmfa_mgr_cd = row["HMFA_MGR_CD"] == DBNull.Value ? string.Empty : row["HMFA_MGR_CD"].ToString(),
                hmfa_mgr_name = row["HMFA_MGR_NAME"] == DBNull.Value ? string.Empty : row["HMFA_MGR_NAME"].ToString(),
                hmfa_acc_dt = row["HMFA_ACC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_ACC_DT"]),
                hmfa_bonus_st_dt = row["HMFA_BONUS_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_BONUS_ST_DT"]),
                hmfa_sr_open_dt = row["HMFA_SR_OPEN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_SR_OPEN_DT"]),
                hmfa_act_stus = row["HMFA_ACT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HMFA_ACT_STUS"]),
                hmfa_cre_by = row["HMFA_CRE_BY"] == DBNull.Value ? string.Empty : row["HMFA_CRE_BY"].ToString(),
                hmfa_cre_dt = row["HMFA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_CRE_DT"]),
                hmfa_mod_by = row["HMFA_MOD_BY"] == DBNull.Value ? string.Empty : row["HMFA_MOD_BY"].ToString(),
                hmfa_mod_dt = row["HMFA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMFA_MOD_DT"]),
                hmfa_pc_cat = row["HMFA_PC_CAT"] == DBNull.Value ? string.Empty : row["HMFA_PC_CAT"].ToString(),
                hmfa_bonus_method = row["HMFA_BONUS_METHOD"] == DBNull.Value ? string.Empty : row["HMFA_BONUS_METHOD"].ToString(),
                hmfa_mainpc = row["HMFA_MAINPC"] == DBNull.Value ? string.Empty : row["HMFA_MAINPC"].ToString()
            };
        }
    }
}
