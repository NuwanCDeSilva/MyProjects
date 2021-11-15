using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class INR_AVG_COST_GIT
    {
        public String Iacg_com { get; set; }
        public String Iacg_loc { get; set; }
        public String Iacg_loc_tp { get; set; }
        public String Iacg_itm_cd { get; set; }
        public String Iacg_itm_stus { get; set; }
        public DateTime Iacg_avg_dt { get; set; }
        public Decimal Iacg_avg_cost { get; set; }
        public Decimal Iacg_avg_qty { get; set; }
        public String Iacg_cre_by { get; set; }
        public DateTime Iacg_cre_dt { get; set; }
        public String Iacg_mod_by { get; set; }
        public DateTime Iacg_mod_dt { get; set; }
        public String Iacg_mod_doc { get; set; }
        public Decimal Iacg_out_qty { get; set; }
        public Decimal Iacg_in_qty { get; set; }
        public Decimal Tmp_mult_cost { get; set; }
        public bool Iacg_direction { get; set; }
        public static INR_AVG_COST_GIT Converter(DataRow row)
        {
            return new INR_AVG_COST_GIT
            {
                Iacg_com = row["IACG_COM"] == DBNull.Value ? string.Empty : row["IACG_COM"].ToString(),
                Iacg_loc_tp = row["IACG_LOC_TP"] == DBNull.Value ? string.Empty : row["IACG_LOC_TP"].ToString(),
                Iacg_itm_cd = row["IACG_ITM_CD"] == DBNull.Value ? string.Empty : row["IACG_ITM_CD"].ToString(),
                Iacg_itm_stus = row["IACG_ITM_STUS"] == DBNull.Value ? string.Empty : row["IACG_ITM_STUS"].ToString(),
                Iacg_avg_dt = row["IACG_AVG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IACG_AVG_DT"].ToString()),
                Iacg_avg_cost = row["IACG_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IACG_AVG_COST"].ToString()),
                Iacg_avg_qty = row["IACG_AVG_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IACG_AVG_QTY"].ToString()),
                Iacg_cre_by = row["IACG_CRE_BY"] == DBNull.Value ? string.Empty : row["IACG_CRE_BY"].ToString(),
                Iacg_cre_dt = row["IACG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IACG_CRE_DT"].ToString()),
                Iacg_mod_by = row["IACG_MOD_BY"] == DBNull.Value ? string.Empty : row["IACG_MOD_BY"].ToString(),
                Iacg_mod_dt = row["IACG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IACG_MOD_DT"].ToString()),
                Iacg_mod_doc = row["IACG_MOD_DOC"] == DBNull.Value ? string.Empty : row["IACG_MOD_DOC"].ToString(),
                Iacg_out_qty = row["IACG_OUT_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IACG_OUT_QTY"].ToString()),
                Iacg_in_qty = row["IACG_IN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IACG_IN_QTY"].ToString())
            };
        } 
    }
}
