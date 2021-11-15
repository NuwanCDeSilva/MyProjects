using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class mst_proc_ele
    {
        public String Mphe_cnty { get; set; }
        public String Mphe_com { get; set; }
        public String Mphe_doc_tp { get; set; }
        public String Mphe_proc_cd { get; set; }
        public String Mphe_cost_cat { get; set; }
        public String Mphe_cost_tp { get; set; }
        public String Mphe_cost_ele { get; set; }
        public Int32 Mphe_act { get; set; }
        public String Mphe_cre_by { get; set; }
        public DateTime Mphe_cre_dt { get; set; }
        public String Mphe_cre_session { get; set; }
        public String Mphe_mod_by { get; set; }
        public DateTime Mphe_mod_dt { get; set; }
        public String Mphe_mod_session { get; set; }
        public Int32 Mphe_mp { get; set; }
        public String Mphe_consin { get; set; }
        public static mst_proc_ele Converter(DataRow row)
        {
            return new mst_proc_ele
            {
                Mphe_cnty = row["MPHE_CNTY"] == DBNull.Value ? string.Empty : row["MPHE_CNTY"].ToString(),
                Mphe_com = row["MPHE_COM"] == DBNull.Value ? string.Empty : row["MPHE_COM"].ToString(),
                Mphe_doc_tp = row["MPHE_DOC_TP"] == DBNull.Value ? string.Empty : row["MPHE_DOC_TP"].ToString(),
                Mphe_proc_cd = row["MPHE_PROC_CD"] == DBNull.Value ? string.Empty : row["MPHE_PROC_CD"].ToString(),
                Mphe_cost_cat = row["MPHE_COST_CAT"] == DBNull.Value ? string.Empty : row["MPHE_COST_CAT"].ToString(),
                Mphe_cost_tp = row["MPHE_COST_TP"] == DBNull.Value ? string.Empty : row["MPHE_COST_TP"].ToString(),
                Mphe_cost_ele = row["MPHE_COST_ELE"] == DBNull.Value ? string.Empty : row["MPHE_COST_ELE"].ToString(),
                Mphe_act = row["MPHE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPHE_ACT"].ToString()),
                Mphe_cre_by = row["MPHE_CRE_BY"] == DBNull.Value ? string.Empty : row["MPHE_CRE_BY"].ToString(),
                Mphe_cre_dt = row["MPHE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPHE_CRE_DT"].ToString()),
                Mphe_cre_session = row["MPHE_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MPHE_CRE_SESSION"].ToString(),
                Mphe_mod_by = row["MPHE_MOD_BY"] == DBNull.Value ? string.Empty : row["MPHE_MOD_BY"].ToString(),
                Mphe_mod_dt = row["MPHE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPHE_MOD_DT"].ToString()),
                Mphe_mod_session = row["MPHE_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MPHE_MOD_SESSION"].ToString(),
                Mphe_mp = row["MPHE_MP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPHE_MP"].ToString()),
                Mphe_consin = row["MPHE_CONSIN"] == DBNull.Value ? string.Empty : row["MPHE_CONSIN"].ToString(),
            };
        }
    }
}

