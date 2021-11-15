using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class ref_eli_comm_targ
    {
        public string rect_com { get; set; }
        public string rect_pc { get; set; }
        public string rect_tp { get; set; }
        public Int32 rect_month { get; set; }
        public decimal rect_target { get; set; }
        public decimal rect_frm_per { get; set; }
        public decimal rect_to_per { get; set; }
        public decimal rect_mng_comm { get; set; }
        public decimal rect_exc_comm { get; set; }
        public decimal rect_cashi_comm { get; set; }
        public decimal rect_help_comm { get; set; }
        public string rect_anal1 { get; set; }
        public string rect_anal2 { get; set; }
        public string rect_anal3 { get; set; }
        public string rect_cre_by { get; set; }
        public DateTime rect_cre_dt { get; set; }
        public Int64 rect_seq { get; set; }

        public static ref_eli_comm_targ Converter(DataRow row)
        {
            return new ref_eli_comm_targ
            {
                rect_com = row["rect_com"] == DBNull.Value ? string.Empty : row["rect_com"].ToString(),
                rect_pc = row["rect_pc"] == DBNull.Value ? string.Empty : row["rect_pc"].ToString(),
                rect_tp = row["rect_tp"] == DBNull.Value ? string.Empty : row["rect_tp"].ToString(),
                rect_month = row["rect_month"] == DBNull.Value ? 0 : Convert.ToInt32(row["rect_month"].ToString()),
                rect_target = row["rect_target"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_target"].ToString()),
                rect_frm_per = row["rect_frm_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_frm_per"].ToString()),
                rect_to_per = row["rect_to_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_to_per"].ToString()),
                rect_mng_comm = row["rect_mng_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_mng_comm"].ToString()),
                rect_exc_comm = row["rect_exc_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_exc_comm"].ToString()),
                rect_cashi_comm = row["rect_cashi_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_cashi_comm"].ToString()),
                rect_help_comm = row["rect_help_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rect_help_comm"].ToString()),
                rect_anal1 = row["rect_anal1"] == DBNull.Value ? string.Empty : row["rect_anal1"].ToString(),
                rect_anal2 = row["rect_anal2"] == DBNull.Value ? string.Empty : row["rect_anal2"].ToString(),
                rect_anal3 = row["rect_anal3"] == DBNull.Value ? string.Empty : row["rect_anal3"].ToString(),
                rect_cre_by = row["rect_cre_by"] == DBNull.Value ? string.Empty : row["rect_cre_by"].ToString(),
                rect_cre_dt = row["rect_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rect_cre_dt"].ToString()),
                rect_seq = row["rect_seq"] == DBNull.Value ? 0 : Convert.ToInt64(row["rect_seq"].ToString()),
            };
        }
    }
}
