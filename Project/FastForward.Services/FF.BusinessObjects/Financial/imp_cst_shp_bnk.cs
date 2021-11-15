using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class imp_cst_shp_bnk
    {
        public string icsb_bank_cd { get; set; }
        public decimal icsb_rate { get; set; }
        public decimal icsb_comm { get; set; }
        public Int32 icsb_foc { get; set; }
        public Int32 icsb_shp_garrnt { get; set; }
        public DateTime icsb_valid_frm { get; set; }
        public DateTime icsb_valid_to { get; set; }
        public string icsb_anal1 { get; set; }
        public string icsb_anal2 { get; set; }
        public string icsb_creby { get; set; }
        public DateTime icsb_credt { get; set; }
        public decimal icsb_cost { get; set; }
        public string icsb_com { get; set; }
        public string icsb_curr { get; set; }
        public string icsb_pay_term { get; set; }
        public string icsb_sub_term { get; set; }
        public decimal icsb_min_bank_chg { get; set; }
        public Int32 icsc_expir_months { get; set; }
        public decimal icsb_drft_chg { get; set; }
        public decimal icsb_shpp_grr_comm { get; set; }
        public decimal icsb_min_shp_grnty { get; set; }
        public decimal icsb_bill_pay_comm { get; set; }
        public decimal icsb_swift_chg_of_bllpay { get; set; }
        public decimal icsc_add_month_chg { get; set; }

        public static imp_cst_shp_bnk Converter(DataRow row)
        {
            return new imp_cst_shp_bnk
            {
                icsb_bank_cd = row["icsb_bank_cd"] == DBNull.Value ? string.Empty : row["icsb_bank_cd"].ToString(),
                icsb_rate = row["icsb_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_rate"].ToString()),
                icsb_comm = row["icsb_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_comm"].ToString()),
                icsb_foc = row["icsb_foc"] == DBNull.Value ? 0 : Convert.ToInt32(row["icsb_foc"].ToString()),
                icsb_shp_garrnt = row["icsb_shp_garrnt"] == DBNull.Value ? 0 : Convert.ToInt32(row["icsb_shp_garrnt"].ToString()),
                icsb_valid_frm = row["icsb_valid_frm"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(row["icsb_valid_frm"]),
                icsb_valid_to = row["icsb_valid_to"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(row["icsb_valid_to"]),
                icsb_anal1 = row["icsb_anal1"] == DBNull.Value ? string.Empty : row["icsb_anal1"].ToString(),
                icsb_anal2 = row["icsb_anal2"] == DBNull.Value ? string.Empty : row["icsb_anal2"].ToString(),
                icsb_creby = row["icsb_creby"] == DBNull.Value ? string.Empty : row["icsb_creby"].ToString(),
                icsb_credt = row["icsb_credt"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(row["icsb_credt"]),
                icsb_cost = row["icsb_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_cost"].ToString()),
                icsb_com = row["icsb_com"] == DBNull.Value ? string.Empty : row["icsb_com"].ToString(),
                icsb_curr = row["icsb_curr"] == DBNull.Value ? string.Empty : row["icsb_curr"].ToString(),
                icsb_pay_term = row["icsb_pay_term"] == DBNull.Value ? string.Empty : row["icsb_pay_term"].ToString(),
                icsb_sub_term = row["icsb_sub_term"] == DBNull.Value ? string.Empty : row["icsb_sub_term"].ToString(),
                icsb_min_bank_chg = row["icsb_min_bank_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_min_bank_chg"].ToString()),
                icsc_expir_months = row["icsc_expir_months"] == DBNull.Value ? 0 : Convert.ToInt32(row["icsc_expir_months"].ToString()),
                icsb_drft_chg = row["icsb_drft_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_drft_chg"].ToString()),
                icsb_shpp_grr_comm = row["icsb_shpp_grr_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_shpp_grr_comm"].ToString()),
                icsb_min_shp_grnty = row["icsb_min_shp_grnty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_min_shp_grnty"].ToString()),
                icsb_bill_pay_comm = row["icsb_bill_pay_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_bill_pay_comm"].ToString()),
                icsb_swift_chg_of_bllpay = row["icsb_swift_chg_of_bllpay"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsb_swift_chg_of_bllpay"].ToString()),
                icsc_add_month_chg = row["icsc_add_month_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["icsc_add_month_chg"].ToString()),
            };
        }
    }
}
