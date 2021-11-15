using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class SUNRECIEPTHDR
    {
       public String sar_receipt_no { get; set; }
       public String sar_receipt_type { get; set; }
       public string sar_seq_no { get; set; }
       public String sar_profit_center_cd { get; set; }
       public decimal sar_tot_settle_amt { get; set; }
        public string sar_debtor_name { get; set; }
        public DateTime sar_receipt_date { get; set; }
        public string sar_debtor_cd { get; set; }
        public string sard_ref_no { get; set; }
        public string checkno { get; set; }
        public string RecieptType { get; set; }

        public string sar_remarks { get; set; }
        public string sard_inv_no { get; set; }
        public string sard_chq_bank_cd { get; set; }
        public string sard_chq_branch { get; set; }
        public string Branchdesc { get; set; }
        public DateTime BankdepositeDate { get; set; }
        public static SUNRECIEPTHDR Converter(DataRow row)
        {
            return new SUNRECIEPTHDR
            {

                sar_receipt_no = row["sar_receipt_no"] == DBNull.Value ? string.Empty : row["sar_receipt_no"].ToString(),
                sar_receipt_type = row["sar_receipt_type"] == DBNull.Value ? string.Empty : row["sar_receipt_type"].ToString(),
                sar_seq_no = row["sar_seq_no"] == DBNull.Value ? string.Empty : row["sar_seq_no"].ToString(),
                sar_profit_center_cd = row["sar_profit_center_cd"] == DBNull.Value ? string.Empty : row["sar_profit_center_cd"].ToString(),
                sar_tot_settle_amt = row["sar_tot_settle_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sar_tot_settle_amt"]),
                sar_debtor_name = row["sar_debtor_name"] == DBNull.Value ? string.Empty : row["sar_debtor_name"].ToString(),
                sar_debtor_cd = row["sar_debtor_cd"] == DBNull.Value ? string.Empty : row["sar_debtor_cd"].ToString(),
                sar_receipt_date = row["sar_receipt_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sar_receipt_date"].ToString()),
                sard_ref_no = row["sard_ref_no"] == DBNull.Value ? string.Empty : row["sard_ref_no"].ToString(),
                checkno = row["checkno"] == DBNull.Value ? string.Empty : row["checkno"].ToString(),
                sar_remarks = row["sar_remarks"] == DBNull.Value ? string.Empty : row["sar_remarks"].ToString(),
                RecieptType = row["RecieptType"] == DBNull.Value ? string.Empty : row["RecieptType"].ToString(),
                sard_inv_no = row["sard_inv_no"] == DBNull.Value ? string.Empty : row["sard_inv_no"].ToString(),
                sard_chq_bank_cd = row["sard_chq_bank_cd"] == DBNull.Value ? string.Empty : row["sard_chq_bank_cd"].ToString(),
                sard_chq_branch = row["sard_chq_branch"] == DBNull.Value ? string.Empty : row["sard_chq_branch"].ToString(),
                Branchdesc = row["Branchdesc"] == DBNull.Value ? string.Empty : row["Branchdesc"].ToString(),
                BankdepositeDate = row["BankdepositeDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BankdepositeDate"].ToString()),
            };
        }
    }
}
