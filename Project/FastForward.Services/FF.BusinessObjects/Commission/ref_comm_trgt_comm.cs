using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ref_comm_trgt_comm
    {
        public string rctc_doc_no { get; set; }
        public string rctc_exec { get; set; }
        public Int32 rctc_month { get; set; }
        public decimal rctc_target { get; set; }
        public decimal rctc_st_per { get; set; }
        public decimal rctc_end_per { get; set; }
        public decimal rctc_exc_rate { get; set; }
        public string rctc_mngr { get; set; }
        public decimal rctc_mngr_rate { get; set; }
        public string rctc_anal1 { get; set; }
        public string rctc_anal2 { get; set; }
        public string rctc_creby { get; set; }
        public DateTime rctc_cre_dt { get; set; }
        public Int32 rctc_stus  { get; set; }
        public string rctc_modby { get; set; }
        public DateTime rctc_mod_dt { get; set; }
        public Int32 rctc_year { get; set; }

        public static ref_comm_trgt_comm Converter(DataRow row)
        {
            return new ref_comm_trgt_comm
            {
                rctc_doc_no = row["rctc_doc_no"] == DBNull.Value ? string.Empty : row["rctc_doc_no"].ToString(),
                rctc_exec = row["rctc_exec"] == DBNull.Value ? string.Empty : row["rctc_exec"].ToString(),
                rctc_month = row["rctc_month"] == DBNull.Value ? 0 : Convert.ToInt32(row["rctc_month"].ToString()),
                rctc_year = row["rctc_year"] == DBNull.Value ? 0 : Convert.ToInt32(row["rctc_year"].ToString()),
                rctc_target = row["rctc_target"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_target"].ToString()),
                rctc_st_per = row["rctc_st_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_st_per"].ToString()),
                rctc_end_per = row["rctc_end_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_end_per"].ToString()),
                rctc_exc_rate = row["rctc_exc_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_exc_rate"].ToString()),
                rctc_mngr = row["rctc_mngr"] == DBNull.Value ? string.Empty : row["rctc_mngr"].ToString(),
                rctc_mngr_rate = row["rctc_mngr_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rctc_mngr_rate"].ToString()),
                rctc_anal1 = row["rctc_anal1"] == DBNull.Value ? string.Empty : row["rctc_anal1"].ToString(),
                rctc_anal2 = row["rctc_anal2"] == DBNull.Value ? string.Empty : row["rctc_anal2"].ToString(),
                rctc_creby = row["rctc_creby"] == DBNull.Value ? string.Empty : row["rctc_creby"].ToString(),
                rctc_cre_dt = row["rctc_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rctc_cre_dt"].ToString()),
                rctc_stus = row["rctc_stus"] == DBNull.Value ? 0 : Convert.ToInt32(row["rctc_stus"].ToString()),
                rctc_modby = row["rctc_modby"] == DBNull.Value ? string.Empty : row["rctc_modby"].ToString(),
                rctc_mod_dt = row["rctc_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rctc_mod_dt"].ToString()),

            };
        }
    }
}
