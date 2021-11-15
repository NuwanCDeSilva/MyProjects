using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
   public class BLtracker
    {
        public string ibi_itm_cd { get; set; }
        public string ibi_model { get; set; }
        public string ibi_itm_stus { get; set; }
        public DateTime ib_bl_dt { get; set; }
        public string ib_doc_no { get; set; }
        public string ib_bl_no { get; set; }
        public string ib_ref_no { get; set; }
        public string CusEntryno { get; set; }
        public DateTime CusEntrydate { get; set; }
        public Int32 Entrybal { get; set; }
        public Int32 ibi_qty { get; set; }
        public Int32 ibi_req_qty { get; set; }

        public Int32 GrnfreeQty { get; set; }
        public Int32 GrnQty { get; set; }
        public Int32 GrnResQty { get; set; }

        public static BLtracker Converter(DataRow row)
        {
            return new BLtracker
            {
                ibi_itm_cd = row["ibi_itm_cd"] == DBNull.Value ? string.Empty : row["ibi_itm_cd"].ToString(),
                ibi_model = row["ibi_model"] == DBNull.Value ? string.Empty : row["ibi_model"].ToString(),
                ibi_itm_stus = row["ibi_itm_stus"] == DBNull.Value ? string.Empty : row["ibi_itm_stus"].ToString(),
                ib_bl_dt = row["ib_bl_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ib_bl_dt"].ToString()),
                ib_doc_no = row["ib_doc_no"] == DBNull.Value ? string.Empty : row["ib_doc_no"].ToString(),
                ib_bl_no = row["ib_bl_no"] == DBNull.Value ? string.Empty : row["ib_bl_no"].ToString(),
                ib_ref_no = row["ib_ref_no"] == DBNull.Value ? string.Empty : row["ib_ref_no"].ToString(),
                CusEntryno = row["CusEntryno"] == DBNull.Value ? string.Empty : row["CusEntryno"].ToString(),
                CusEntrydate = row["CusEntrydate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CusEntrydate"].ToString()),
                Entrybal = row["Entrybal"] == DBNull.Value ? 0 : Convert.ToInt32(row["Entrybal"]),
                ibi_qty = row["ibi_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["ibi_qty"]),
                ibi_req_qty = row["ibi_req_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["ibi_req_qty"]),
                GrnfreeQty = row["GrnfreeQty"] == DBNull.Value ? 0 : Convert.ToInt32(row["GrnfreeQty"]),
                GrnQty = row["GrnQty"] == DBNull.Value ? 0 : Convert.ToInt32(row["GrnQty"]),
                GrnResQty = row["GrnResQty"] == DBNull.Value ? 0 : Convert.ToInt32(row["GrnResQty"]),
              
            };
        }
    }
}
