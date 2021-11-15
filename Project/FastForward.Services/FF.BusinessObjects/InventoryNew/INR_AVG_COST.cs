using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class INR_AVG_COST
    {
        public String Iac_com { get; set; }
        public String Iac_loc_tp { get; set; }
        public String Iac_itm_cd { get; set; }
        public String Iac_itm_stus { get; set; }
        public DateTime Iac_avg_dt { get; set; }
        public Decimal Iac_avg_cost { get; set; }
        public Decimal Iac_avg_qty { get; set; }
        public String Iac_cre_by { get; set; }
        public DateTime Iac_cre_dt { get; set; }
        public String Iac_mod_by { get; set; }
        public DateTime Iac_mod_dt { get; set; }
        public String Iac_anal_1 { get; set; }
        public String Iac_anal_2 { get; set; }
        public String Iac_anal_3 { get; set; }
        public String Iac_anal_4 { get; set; }
        public String Iac_anal_5 { get; set; }
        public Decimal Iac_costofsale { get; set; }
        public Decimal Iac_tot_inv_rate { get; set; }
        public Decimal Iac_sold_qty { get; set; }
        public Decimal Iac_sold_qty_intr_com { get; set; }
        public Decimal Iac_avg_inv_days { get; set; }
        public Decimal Tmp_mult_cost { get; set; }
        public static INR_AVG_COST Converter(DataRow row)
        {
            return new INR_AVG_COST
            {
                Iac_com = row["IAC_COM"] == DBNull.Value ? string.Empty : row["IAC_COM"].ToString(),
                Iac_loc_tp = row["IAC_LOC_TP"] == DBNull.Value ? string.Empty : row["IAC_LOC_TP"].ToString(),
                Iac_itm_cd = row["IAC_ITM_CD"] == DBNull.Value ? string.Empty : row["IAC_ITM_CD"].ToString(),
                Iac_itm_stus = row["IAC_ITM_STUS"] == DBNull.Value ? string.Empty : row["IAC_ITM_STUS"].ToString(),
                Iac_avg_dt = row["IAC_AVG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IAC_AVG_DT"].ToString()),
                Iac_avg_cost = row["IAC_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_AVG_COST"].ToString()),
                Iac_avg_qty = row["IAC_AVG_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_AVG_QTY"].ToString()),
                Iac_cre_by = row["IAC_CRE_BY"] == DBNull.Value ? string.Empty : row["IAC_CRE_BY"].ToString(),
                Iac_cre_dt = row["IAC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IAC_CRE_DT"].ToString()),
                Iac_mod_by = row["IAC_MOD_BY"] == DBNull.Value ? string.Empty : row["IAC_MOD_BY"].ToString(),
                Iac_mod_dt = row["IAC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IAC_MOD_DT"].ToString()),
                Iac_anal_1 = row["IAC_ANAL_1"] == DBNull.Value ? string.Empty : row["IAC_ANAL_1"].ToString(),
                Iac_anal_2 = row["IAC_ANAL_2"] == DBNull.Value ? string.Empty : row["IAC_ANAL_2"].ToString(),
                Iac_anal_3 = row["IAC_ANAL_3"] == DBNull.Value ? string.Empty : row["IAC_ANAL_3"].ToString(),
                Iac_anal_4 = row["IAC_ANAL_4"] == DBNull.Value ? string.Empty : row["IAC_ANAL_4"].ToString(),
                Iac_anal_5 = row["IAC_ANAL_5"] == DBNull.Value ? string.Empty : row["IAC_ANAL_5"].ToString(),
                Iac_costofsale = row["IAC_COSTOFSALE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_COSTOFSALE"].ToString()),
                Iac_tot_inv_rate = row["IAC_TOT_INV_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_TOT_INV_RATE"].ToString()),
                Iac_sold_qty = row["IAC_SOLD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_SOLD_QTY"].ToString()),
                Iac_sold_qty_intr_com = row["IAC_SOLD_QTY_INTR_COM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_SOLD_QTY_INTR_COM"].ToString()),
                Iac_avg_inv_days = row["IAC_AVG_INV_DAYS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IAC_AVG_INV_DAYS"].ToString())
            };
        }
    }
}
