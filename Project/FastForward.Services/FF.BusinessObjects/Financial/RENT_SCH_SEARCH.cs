using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class RENT_SCH_SEARCH
    {
        public string psh_no { get; set; }
        public string psh_pc { get; set; }
        public string psh_pay_tp { get; set; }
        public string psh_pay_sb_tp { get; set; }
        public string psh_add1 { get; set; }
        public string psh_add2 { get; set; }
        public string psh_anl1 { get; set; }
        public string psh_dist { get; set; }
        public string psh_rmk { get; set; }
        public string psh_prv { get; set; }
        public string psh_cr_acc { get; set; }
        public string psh_dr_acc { get; set; }
        public string psh_ref_no { get; set; }
        public string psh_frm_dt { get; set; }
        public string psh_to_dt { get; set; }
        public string psh_stus { get; set; }
        public string psh_trm { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static RENT_SCH_SEARCH Converter(DataRow row)
        {
            return new RENT_SCH_SEARCH
            {
                psh_no = row["psh_no"] == DBNull.Value ? string.Empty : row["psh_no"].ToString(),
                psh_pc = row["psh_pc"] == DBNull.Value ? string.Empty : row["psh_pc"].ToString(),
                psh_pay_tp = row["psh_pay_tp"] == DBNull.Value ? string.Empty : row["psh_pay_tp"].ToString(),
                psh_pay_sb_tp = row["psh_pay_sb_tp"] == DBNull.Value ? string.Empty : row["psh_pay_sb_tp"].ToString(),
                psh_add1 = row["psh_add1"] == DBNull.Value ? string.Empty : row["psh_add1"].ToString(),
                psh_add2 = row["psh_add2"] == DBNull.Value ? string.Empty : row["psh_add2"].ToString(),
                psh_anl1 = row["psh_anl1"] == DBNull.Value ? string.Empty : row["psh_anl1"].ToString(),
                psh_dist = row["psh_dist"] == DBNull.Value ? string.Empty : row["psh_dist"].ToString(),
                psh_rmk = row["psh_rmk"] == DBNull.Value ? string.Empty : row["psh_rmk"].ToString(),
                psh_prv = row["psh_prv"] == DBNull.Value ? string.Empty : row["psh_prv"].ToString(),
                psh_cr_acc = row["psh_cr_acc"] == DBNull.Value ? string.Empty : row["psh_cr_acc"].ToString(),
                psh_dr_acc = row["psh_dr_acc"] == DBNull.Value ? string.Empty : row["psh_dr_acc"].ToString(),
                psh_ref_no = row["psh_ref_no"] == DBNull.Value ? string.Empty : row["psh_ref_no"].ToString(),
                psh_frm_dt = row["psh_frm_dt"] == DBNull.Value ? string.Empty : row["psh_frm_dt"].ToString(),
                psh_to_dt = row["psh_to_dt"] == DBNull.Value ? string.Empty : row["psh_to_dt"].ToString(),
                psh_stus = row["psh_stus"] == DBNull.Value ? string.Empty : row["psh_stus"].ToString(),
                psh_trm = row["psh_trm"] == DBNull.Value ? string.Empty : row["psh_trm"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
