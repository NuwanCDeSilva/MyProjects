using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
 public   class SAT_ADJ_CRCD
    {
        public Int64 staj_seq { get; set; }
        public string staj_com { get; set; }
        public string staj_pc { get; set; }
        public DateTime staj_dt { get; set; }
        public Int32 staj_direct { get; set; }
        public string staj_tp { get; set; }
        public string staj_sub_tp { get; set; }
        public decimal staj_amt { get; set; }
        public string staj_acc_dbt { get; set; }
        public string staj_acc_crd { get; set; }
        public string staj_rmk { get; set; }
        public string staj_ref { get; set; }
        public Int64 staj_ref_seq { get; set; }
        public string staj_cre_by { get; set; }
        public DateTime staj_cre_dt { get; set; }
        public string staj_bankcd { get; set; }
        public string staj_midno { get; set; }
        public int staj_is_upload { get; set; }
        public DateTime staj_state_date { get; set; }

        public static SAT_ADJ_CRCD Converter(DataRow row)
        {
            return new SAT_ADJ_CRCD
            {
                staj_seq = row["staj_seq"] == DBNull.Value ? 0 : Convert.ToInt64(row["staj_seq"]),
                staj_com = row["staj_com"] == DBNull.Value ? string.Empty : row["staj_com"].ToString(),
                staj_pc = row["staj_pc"] == DBNull.Value ? string.Empty : row["staj_pc"].ToString(),
                staj_dt = row["staj_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["staj_dt"].ToString()),
                staj_direct = row["staj_direct"] == DBNull.Value ? 0 : Convert.ToInt32(row["staj_direct"]),
                staj_tp = row["staj_tp"] == DBNull.Value ? string.Empty : row["staj_tp"].ToString(),
                staj_sub_tp = row["staj_sub_tp"] == DBNull.Value ? string.Empty : row["staj_sub_tp"].ToString(),
                staj_amt = row["staj_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["staj_amt"]),
                staj_acc_dbt = row["staj_acc_dbt"] == DBNull.Value ? string.Empty : row["staj_acc_dbt"].ToString(),
                staj_acc_crd = row["staj_acc_crd"] == DBNull.Value ? string.Empty : row["staj_acc_crd"].ToString(),
                staj_rmk = row["staj_rmk"] == DBNull.Value ? string.Empty : row["staj_rmk"].ToString(),
                staj_ref = row["staj_ref"] == DBNull.Value ? string.Empty : row["staj_ref"].ToString(),
                staj_ref_seq = row["staj_ref_seq"] == DBNull.Value ? 0 : Convert.ToInt64(row["staj_ref_seq"]),
                staj_cre_by = row["staj_cre_by"] == DBNull.Value ? string.Empty : row["staj_cre_by"].ToString(),
                staj_cre_dt = row["staj_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["staj_cre_dt"].ToString()),
                staj_bankcd = row["staj_bankcd"] == DBNull.Value ? string.Empty : row["staj_bankcd"].ToString(),
                staj_midno = row["staj_midno"] == DBNull.Value ? string.Empty : row["staj_midno"].ToString(),
                staj_is_upload = row["staj_is_upload"] == DBNull.Value ? 0 : Convert.ToInt16(row["staj_is_upload"]),
                staj_state_date = row["staj_state_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["staj_state_date"].ToString()),

            };
        }


    }
}
