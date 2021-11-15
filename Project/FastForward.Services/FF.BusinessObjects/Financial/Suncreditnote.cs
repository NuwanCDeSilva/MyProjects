using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class Suncreditnote
    {
        public string grah_loc { get; set; }
        public string ml_loc_desc { get; set; }
        public DateTime req_date { get; set; }
        public string grah_ref { get; set; }
        public string job_no { get; set; }
        public decimal cr_amt { get; set; }
        public string sjb_b_cust_cd { get; set; }
        public string sjb_b_cust_name { get; set; }
        public string ith_doc_no { get; set; }
        public DateTime ith_doc_date { get; set; }
        public string ml_def_pc { get; set; }
        public Int32 ith_direct { get; set; }
        public string grah_fuc_cd { get; set; }
        public string sah_cus_cd { get; set; }
        public string tax_cd { get; set; }
        public string sah_pc { get; set; }
        public string sah_inv_tp { get; set; }
         public static Suncreditnote Converter(DataRow row)
        {
            return new Suncreditnote
            {
                grah_loc = row["grah_loc"] == DBNull.Value ? string.Empty : row["grah_loc"].ToString(),
                ml_loc_desc = row["ml_loc_desc"] == DBNull.Value ? string.Empty : row["ml_loc_desc"].ToString(),
                req_date = row["req_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["req_date"]),
                grah_ref = row["grah_ref"] == DBNull.Value ? string.Empty : row["grah_ref"].ToString(),
                job_no = row["job_no"] == DBNull.Value ? string.Empty : row["job_no"].ToString(),
                cr_amt = row["cr_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cr_amt"]),
                sjb_b_cust_cd = row["sjb_b_cust_cd"] == DBNull.Value ? string.Empty : row["sjb_b_cust_cd"].ToString(),
                sjb_b_cust_name = row["sjb_b_cust_name"] == DBNull.Value ? string.Empty : row["sjb_b_cust_name"].ToString(),
                ith_doc_no = row["ith_doc_no"] == DBNull.Value ? string.Empty : row["ith_doc_no"].ToString(),
                ith_doc_date = row["ith_doc_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ith_doc_date"]),
                ml_def_pc = row["ml_def_pc"] == DBNull.Value ? string.Empty : row["ml_def_pc"].ToString(),
                ith_direct = row["ith_direct"] == DBNull.Value ? 0 : Convert.ToInt32(row["ith_direct"]),
                grah_fuc_cd = row["grah_fuc_cd"] == DBNull.Value ? string.Empty : row["grah_fuc_cd"].ToString(),
                sah_cus_cd = row["sah_cus_cd"] == DBNull.Value ? string.Empty : row["sah_cus_cd"].ToString(),
                tax_cd = row["tax_cd"] == DBNull.Value ? string.Empty : row["tax_cd"].ToString(),
                sah_pc = row["sah_pc"] == DBNull.Value ? string.Empty : row["sah_pc"].ToString(),
                sah_inv_tp = row["sah_inv_tp"] == DBNull.Value ? string.Empty : row["sah_inv_tp"].ToString(),
            };
         }
    }
}
