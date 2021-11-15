using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{

    //Kelum : Office Center Assign for Customers : 2016-May-02

    [Serializable]
    public class MasterBusinessOfficeEntry
    {
        public String _mbbo_com { get; set; }
        public String _mbbo_cd { get; set; }
        public String _mbbo_tp { get; set; }
        public Int32 _mbbo_direct { get; set; }
        public String _mbbo_off_cd { get; set; }
        public Int32 _mbbo_act { get; set; }
        public String _mbbo_cre_by { get; set; }
        public DateTime _mbbo_cre_dt { get; set; }
        public String _mbbo_mod_by { get; set; }
        public DateTime _mbbo_mod_dt { get; set; }

        public static MasterBusinessOfficeEntry Converter(DataRow row)
        {
            return new MasterBusinessOfficeEntry
            {
                _mbbo_com = row["MBBO_COM"] == DBNull.Value ? string.Empty : row["MBBO_COM"].ToString(),
                _mbbo_cd = row["MBBO_CD"] == DBNull.Value ? string.Empty : row["MBBO_CD"].ToString(),
                _mbbo_tp = row["MBBO_TP"] == DBNull.Value ? string.Empty : row["MBBO_TP"].ToString(),
                _mbbo_direct = row["MBBO_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBBO_DIRECT"].ToString()),
                _mbbo_off_cd = row["MBBO_OFF_CD"] == DBNull.Value ? string.Empty : row["MBBO_OFF_CD"].ToString(),
                _mbbo_act = row["MBBO_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBBO_ACT"].ToString()),
                _mbbo_cre_by = row["MBBO_CRE_BY"] == DBNull.Value ? string.Empty : row["MBBO_CRE_BY"].ToString(),
                _mbbo_cre_dt = row["MBBO_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBBO_CRE_DT"].ToString()),
                _mbbo_mod_by = row["MBBO_MOD_BY"] == DBNull.Value ? string.Empty : row["MBBO_MOD_BY"].ToString(),
                _mbbo_mod_dt = row["MBBO_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBBO_MOD_DT"].ToString()),
               
           };
        }
    }
}


