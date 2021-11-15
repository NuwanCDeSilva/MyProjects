using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
  public  class SUNINVHDR
    {
        public String sah_inv_no { get; set; }
        public String sah_inv_tp { get; set; }
        public string sah_inv_sub_tp { get; set; }
        public String sah_tp { get; set; }
        public decimal total { get; set; }
        public decimal totalunit { get; set; }
        public decimal taxtotal { get; set; }
        public string sah_seq_no { get; set; }
        public string sah_pc { get; set; }
        public string sah_cus_name { get; set; }
        public DateTime sah_dt { get; set; }
        public string sah_cus_cd { get; set; }
        public Int32 sah_direct { get; set; }
        public string sah_man_ref { get; set; }
        public string tax_cd { get; set; }
        public string isdiliver { get; set; }
        public string sah_ref_doc { get; set; }
        public string sah_sales_ex_cd { get; set; }
        public Int32 sah_is_svat { get; set; }
        public string EPF { get; set; }
        public string CODE { get; set; }
        public decimal RealTotalwithtax { get; set; }

        public decimal NBTValue { get; set; }


        public string svatcd { get; set; }

        public Int32 sah_tax_exempted { get; set; }
        public string sah_anal_4 { get; set; }
        public string sad_itm_cd { get; set; }
        public static SUNINVHDR Converter(DataRow row)
        {
            return new SUNINVHDR
            {

                sah_inv_no = row["sah_inv_no"] == DBNull.Value ? string.Empty : row["sah_inv_no"].ToString(),
                sah_inv_tp = row["sah_inv_tp"] == DBNull.Value ? string.Empty : row["sah_inv_tp"].ToString(),
                sah_inv_sub_tp = row["sah_inv_sub_tp"] == DBNull.Value ? string.Empty : row["sah_inv_sub_tp"].ToString(),
                sah_tp = row["sah_tp"] == DBNull.Value ? string.Empty : row["sah_tp"].ToString(),
                total = row["total"] == DBNull.Value ? 0 : Convert.ToDecimal(row["total"]),
                totalunit = row["totalunit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["totalunit"]),
                taxtotal = row["taxtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(row["taxtotal"]),
                sah_seq_no = row["sah_seq_no"] == DBNull.Value ? string.Empty : row["sah_seq_no"].ToString(),
                sah_pc = row["sah_pc"] == DBNull.Value ? string.Empty : row["sah_pc"].ToString(),
                sah_cus_name = row["sah_cus_name"] == DBNull.Value ? string.Empty : row["sah_cus_name"].ToString(),
                sah_dt = row["sah_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sah_dt"].ToString()),
                sah_cus_cd = row["sah_cus_cd"] == DBNull.Value ? string.Empty : row["sah_cus_cd"].ToString(),
                sah_direct = row["sah_direct"] == DBNull.Value ? 0 : Convert.ToInt32(row["sah_direct"]),
                sah_man_ref = row["sah_man_ref"] == DBNull.Value ? string.Empty : row["sah_man_ref"].ToString(),
                tax_cd = row["tax_cd"] == DBNull.Value ? string.Empty : row["tax_cd"].ToString(),
                isdiliver = row["isdiliver"] == DBNull.Value ? string.Empty : row["isdiliver"].ToString(),
                sah_ref_doc = row["sah_ref_doc"] == DBNull.Value ? string.Empty : row["sah_ref_doc"].ToString(),
                sah_sales_ex_cd = row["sah_sales_ex_cd"] == DBNull.Value ? string.Empty : row["sah_sales_ex_cd"].ToString(),
                sah_is_svat = row["sah_is_svat"] == DBNull.Value ? 0 : Convert.ToInt32(row["sah_is_svat"]),
                EPF = row["EPF"] == DBNull.Value ? string.Empty : row["EPF"].ToString(),
                CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                RealTotalwithtax = row["RealTotalwithtax"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RealTotalwithtax"]),
                svatcd = row["svatcd"] == DBNull.Value ? string.Empty : row["svatcd"].ToString(),
                NBTValue = row["NBTValue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["NBTValue"]),
                sah_tax_exempted = row["sah_tax_exempted"] == DBNull.Value ? 0 : Convert.ToInt32(row["sah_tax_exempted"]),
                sah_anal_4 = row["sah_anal_4"] == DBNull.Value ? string.Empty : row["sah_anal_4"].ToString(),
                sad_itm_cd = row["sad_itm_cd"] == DBNull.Value ? string.Empty : row["sad_itm_cd"].ToString(),

            };
        }

    }
}
