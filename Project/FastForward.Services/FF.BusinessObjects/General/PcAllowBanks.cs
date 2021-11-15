using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class PcAllowBanks
    {
        public string Mpab_Com { get; set; }
        public string Mpab_Pc { get; set; }
        public string Mpab_Loc { get; set; }
        public string Mpab_Bnk_Cd { get; set; }
        public Int32 mpab_Is_Active { get; set; }
        public string Mpab_Cre_By { get; set; }
        public DateTime mpab_Cre_Dt { get; set; }
        public string Mpab_Mod_By { get; set; }
        public DateTime Mpab_Mod_Dt { get; set; }

        public static PcAllowBanks Converter(DataRow row)
        {
            return new PcAllowBanks
            {
                Mpab_Com = row["mpab_com"] == DBNull.Value ? string.Empty : row["mpab_com"].ToString(),
                Mpab_Pc = row["mpab_pc"] == DBNull.Value ? string.Empty : row["mpab_pc"].ToString(),
                Mpab_Loc = row["mpab_loc"] == DBNull.Value ? string.Empty : row["mpab_loc"].ToString(),
                Mpab_Bnk_Cd = row["mpab_bnk_cd"] == DBNull.Value ? string.Empty : row["mpab_bnk_cd"].ToString(),
                mpab_Is_Active = row["mpab_is_active"] == DBNull.Value ? 0 :Convert.ToInt32( row["mpab_is_active"].ToString()),
                Mpab_Cre_By = row["mpab_cre_by"] == DBNull.Value ? string.Empty : row["mpab_cre_by"].ToString(),
                mpab_Cre_Dt = row["mpab_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpab_cre_dt"].ToString()),
                Mpab_Mod_By = row["mpab_mod_by"] == DBNull.Value ? string.Empty : row["mpab_mod_by"].ToString(),
                Mpab_Mod_Dt = row["mpab_mod_dt"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime( row["mpab_mod_dt"].ToString())
            };
        }

    }
}
