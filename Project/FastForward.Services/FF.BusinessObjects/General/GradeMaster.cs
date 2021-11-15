using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class GradeMaster
    {
        public String Mg_com { get; set; }
        public String Mg_chnl { get; set; }
        public String Mg_cd { get; set; }
        public String Mg_desc { get; set; }
        public Int32 Mg_act { get; set; }
        public String Mg_cre_by { get; set; }
        public DateTime Mg_cre_dt { get; set; }
        public String Mg_cre_session { get; set; }
        public String Mg_mod_by { get; set; }
        public DateTime Mg_mod_dt { get; set; }
        public String Mg_mod_session { get; set; }
        public static GradeMaster Converter(DataRow row)
        {
            return new GradeMaster
            {
                Mg_com = row["MG_COM"] == DBNull.Value ? string.Empty : row["MG_COM"].ToString(),
                Mg_chnl = row["MG_CHNL"] == DBNull.Value ? string.Empty : row["MG_CHNL"].ToString(),
                Mg_cd = row["MG_CD"] == DBNull.Value ? string.Empty : row["MG_CD"].ToString(),
                Mg_desc = row["MG_DESC"] == DBNull.Value ? string.Empty : row["MG_DESC"].ToString(),
                Mg_act = row["MG_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MG_ACT"].ToString()),
                Mg_cre_by = row["MG_CRE_BY"] == DBNull.Value ? string.Empty : row["MG_CRE_BY"].ToString(),
                Mg_cre_dt = row["MG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MG_CRE_DT"].ToString()),
                Mg_cre_session = row["MG_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MG_CRE_SESSION"].ToString(),
                Mg_mod_by = row["MG_MOD_BY"] == DBNull.Value ? string.Empty : row["MG_MOD_BY"].ToString(),
                Mg_mod_dt = row["MG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MG_MOD_DT"].ToString()),
                Mg_mod_session = row["MG_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MG_MOD_SESSION"].ToString()
            };
        }
    }
}