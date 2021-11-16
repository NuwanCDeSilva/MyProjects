using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    //===========================================================================================================
    // Computer :- ITPD18  | User :- subodanam On 22-Jun-2017 09:44:51
    //===========================================================================================================

    public class trn_req_cus_det
    {
        public String Rc_seq_no { get; set; }
        public String Rc_cus_cd { get; set; }
        public String Rc_cus_tp { get; set; }
        public String Rc_exe_cd { get; set; }
        public String Rc_cre_by { get; set; }
        public DateTime Rc_cre_dt { get; set; }
        public String Rc_mod_by { get; set; }
        public DateTime Rc_mod_dt { get; set; }
        public static trn_req_cus_det Converter(DataRow row)
        {
            return new trn_req_cus_det
            {
                Rc_seq_no = row["RC_SEQ_NO"] == DBNull.Value ? string.Empty : row["RC_SEQ_NO"].ToString(),
                Rc_cus_cd = row["RC_CUS_CD"] == DBNull.Value ? string.Empty : row["RC_CUS_CD"].ToString(),
                Rc_cus_tp = row["RC_CUS_TP"] == DBNull.Value ? string.Empty : row["RC_CUS_TP"].ToString(),
                Rc_exe_cd = row["RC_EXE_CD"] == DBNull.Value ? string.Empty : row["RC_EXE_CD"].ToString(),
                Rc_cre_by = row["RC_CRE_BY"] == DBNull.Value ? string.Empty : row["RC_CRE_BY"].ToString(),
                Rc_cre_dt = row["RC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RC_CRE_DT"].ToString()),
                Rc_mod_by = row["RC_MOD_BY"] == DBNull.Value ? string.Empty : row["RC_MOD_BY"].ToString(),
                Rc_mod_dt = row["RC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RC_MOD_DT"].ToString())
            };
        }
    }
}
