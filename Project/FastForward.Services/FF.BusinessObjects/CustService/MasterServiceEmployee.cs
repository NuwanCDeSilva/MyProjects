using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 12-Feb-2015 12:06:19
    //===========================================================================================================


    public class MasterServiceEmployee
    {
        public Int32 Msi_seq { get; set; }
        public String Msi_com { get; set; }
        public String Msi_pty_tp { get; set; }
        public String Msi_pty_cd { get; set; }
        public String Msi_emp { get; set; }
        public String Msi_prnt_cate { get; set; }
        public String Msi_prnt_emp { get; set; }
        public Int32 Msi_act { get; set; }
        public String Msi_cre_by { get; set; }
        public DateTime Msi_cre_dt { get; set; }
        public String Msi_mod_by { get; set; }
        public DateTime Msi_mod_dt { get; set; }
        public static MasterServiceEmployee Converter(DataRow row)
        {
            return new MasterServiceEmployee
            {
                Msi_seq = row["MSI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSI_SEQ"].ToString()),
                Msi_com = row["MSI_COM"] == DBNull.Value ? string.Empty : row["MSI_COM"].ToString(),
                Msi_pty_tp = row["MSI_PTY_TP"] == DBNull.Value ? string.Empty : row["MSI_PTY_TP"].ToString(),
                Msi_pty_cd = row["MSI_PTY_CD"] == DBNull.Value ? string.Empty : row["MSI_PTY_CD"].ToString(),
                Msi_emp = row["MSI_EMP"] == DBNull.Value ? string.Empty : row["MSI_EMP"].ToString(),
                Msi_prnt_cate = row["MSI_PRNT_CATE"] == DBNull.Value ? string.Empty : row["MSI_PRNT_CATE"].ToString(),
                Msi_prnt_emp = row["MSI_PRNT_EMP"] == DBNull.Value ? string.Empty : row["MSI_PRNT_EMP"].ToString(),
                Msi_act = row["MSI_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSI_ACT"].ToString()),
                Msi_cre_by = row["MSI_CRE_BY"] == DBNull.Value ? string.Empty : row["MSI_CRE_BY"].ToString(),
                Msi_cre_dt = row["MSI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSI_CRE_DT"].ToString()),
                Msi_mod_by = row["MSI_MOD_BY"] == DBNull.Value ? string.Empty : row["MSI_MOD_BY"].ToString(),
                Msi_mod_dt = row["MSI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSI_MOD_DT"].ToString())
            };
        }
    }
}
