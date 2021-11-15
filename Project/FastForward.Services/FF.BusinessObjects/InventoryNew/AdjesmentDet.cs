using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
  public  class AdjesmentDet
    {
        public string ith_loc { get; set; }
        public decimal itb_unit_cost { get; set; }
        public Int32 itb_qty { get; set; }
        public string ith_doc_tp { get; set; }
        public string ith_stus { get; set; }
        public DateTime ith_doc_date { get; set; }
        public string ith_com { get; set; }
        public string ml_ope_cd { get; set; }
        public string ml_cate_2 { get; set; }
        public string ith_sub_tp { get; set; }
        public Int32 ith_direct { get; set; }
        public decimal total { get; set; }


          public static AdjesmentDet Converter(DataRow row)
        {
            return new AdjesmentDet
            {
                ith_loc = row["ith_loc"] == DBNull.Value ? string.Empty : row["ith_loc"].ToString(),
                itb_unit_cost = row["itb_unit_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(row["itb_unit_cost"]),
                itb_qty = row["itb_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["itb_qty"]),
                ith_doc_tp = row["ith_doc_tp"] == DBNull.Value ? string.Empty : row["ith_doc_tp"].ToString(),
                ith_stus = row["ith_stus"] == DBNull.Value ? string.Empty : row["ith_stus"].ToString(),
                ith_doc_date = row["ith_doc_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ith_doc_date"].ToString()),
                ith_com = row["ith_com"] == DBNull.Value ? string.Empty : row["ith_com"].ToString(),
                ml_ope_cd = row["ml_ope_cd"] == DBNull.Value ? string.Empty : row["ml_ope_cd"].ToString(),
                ml_cate_2 = row["ml_cate_2"] == DBNull.Value ? string.Empty : row["ml_cate_2"].ToString(),
                ith_sub_tp = row["ith_sub_tp"] == DBNull.Value ? string.Empty : row["ith_sub_tp"].ToString(),
                ith_direct = row["ith_direct"] == DBNull.Value ? 0 : Convert.ToInt32(row["ith_direct"]),
                total = row["total"] == DBNull.Value ? 0 : Convert.ToDecimal(row["total"]),

            };
          }
    }
}
