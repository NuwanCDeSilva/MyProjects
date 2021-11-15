using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
  public  class LocPurSun
    {
        public string itb_doc_no { get; set; }
        public string itb_com { get; set; }
        public Int32 itb_qty { get; set; }
        public Int64 ith_seq_no { get; set; }
        public DateTime ith_doc_date { get; set; }
        public string ith_bus_entity { get; set; }
        public decimal pod_unit_price { get; set; }
        public decimal pod_dis_amt { get; set; }
        public decimal pod_line_tax { get; set; }
        public string mbe_name { get; set; }
        public string invoiceno { get; set; }
        public string itb_itm_cd { get; set; }
        public decimal total { get; set; }

        public static LocPurSun Converter(DataRow row)
        {
            return new LocPurSun
            {
                itb_doc_no = row["itb_doc_no"] == DBNull.Value ? string.Empty : row["itb_doc_no"].ToString(),
                itb_com = row["itb_com"] == DBNull.Value ? string.Empty : row["itb_com"].ToString(),
                itb_qty = row["itb_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["itb_qty"]),
                ith_seq_no = row["ith_seq_no"] == DBNull.Value ? 0 : Convert.ToInt64(row["ith_seq_no"]),
                ith_doc_date = row["ith_doc_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ith_doc_date"].ToString()),
                ith_bus_entity = row["ith_bus_entity"] == DBNull.Value ? string.Empty : row["ith_bus_entity"].ToString(),
                pod_unit_price = row["pod_unit_price"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pod_unit_price"]),
                pod_dis_amt = row["pod_dis_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pod_dis_amt"]),
                pod_line_tax = row["pod_line_tax"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pod_line_tax"]),
                mbe_name = row["mbe_name"] == DBNull.Value ? string.Empty : row["mbe_name"].ToString(),
                invoiceno = row["invoiceno"] == DBNull.Value ? string.Empty : row["invoiceno"].ToString(),
                itb_itm_cd = row["itb_itm_cd"] == DBNull.Value ? string.Empty : row["itb_itm_cd"].ToString(),
                total = row["total"] == DBNull.Value ? 0 : Convert.ToDecimal(row["total"]),
            };
        }



    }
}
