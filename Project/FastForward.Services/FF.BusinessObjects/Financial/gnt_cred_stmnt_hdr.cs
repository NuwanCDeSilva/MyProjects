using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table: hpr_ars_dt_defn
    //created by :Shani 21-11-2012
    [Serializable]
    public class gnt_cred_stmnt_hdr
    {
        public Int32 cred_seq { get; set; }
        public string cred_sth_com {get; set;}
        public DateTime cred_sth_dt {get; set;} 
        public string cred_sth_bank {get; set;}
        public string cred_sth_mid {get; set;}
        public string cred_sth_accno {get; set;}
        public Decimal cred_sth_opbal {get; set;}
        public Decimal cred_sth_realizes {get; set;}
        public Decimal cred_sth_prv_realize {get; set;}
        public Decimal cred_sth_cc {get; set;}
        public Decimal cred_sth_adj {get; set;}
        public Decimal cred_sth_clbal {get; set;}
        public Decimal cred_sth_state_bal {get; set;}
        public string cred_sth_stus {get; set;}
        public string cred_sth_cre_by {get; set;}
        public string cred_sth_cre_session {get; set;}
        public string cred_sth_mod_by {get; set;}
        public string cred_sth_mod_session {get; set;}


        public static gnt_cred_stmnt_hdr Converter(DataRow row)
        {
            return new gnt_cred_stmnt_hdr
            {
                cred_seq = row["cred_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["cred_seq"].ToString()),
                cred_sth_com = row["cred_sth_com"] == DBNull.Value ? string.Empty : row["cred_sth_com"].ToString(),
                cred_sth_dt = row["cred_sth_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["cred_sth_dt"]),
                cred_sth_bank = row["cred_sth_bank"] == DBNull.Value ? string.Empty : row["cred_sth_bank"].ToString(),
                cred_sth_mid = row["cred_sth_mid"] == DBNull.Value ? string.Empty : row["cred_sth_mid"].ToString(),
                cred_sth_accno = row["cred_sth_accno"] == DBNull.Value ? string.Empty : row["cred_sth_accno"].ToString(),
                cred_sth_opbal = row["cred_sth_opbal"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_opbal"]),
                cred_sth_realizes = row["cred_sth_realizes"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_realizes"]),
                cred_sth_prv_realize = row["cred_sth_prv_realize"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_prv_realize"]),
                cred_sth_cc = row["cred_sth_cc"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_cc"]),
                cred_sth_adj = row["cred_sth_adj"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_adj"]),
                cred_sth_clbal = row["cred_sth_clbal"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_clbal"]),
                cred_sth_state_bal = row["cred_sth_state_bal"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cred_sth_state_bal"]),
                cred_sth_stus = row["cred_sth_stus"] == DBNull.Value ? string.Empty : row["cred_sth_stus"].ToString(),
                cred_sth_cre_by = row["cred_sth_cre_by"] == DBNull.Value ? string.Empty : row["cred_sth_cre_by"].ToString(),
                cred_sth_mod_by = row["cred_sth_mod_by"] == DBNull.Value ? string.Empty : row["cred_sth_mod_by"].ToString()

            };
        }

    }
}
