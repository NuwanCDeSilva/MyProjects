using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
  public  class InventorySearchItemsAll
    {
         public String iti_doc_no { get; set; }
         public String iti_itm_cd { get; set; }
        public Int32 iti_qty { get; set; }

        public static InventorySearchItemsAll Converter(DataRow row)
        {
            return new InventorySearchItemsAll
            {
                iti_doc_no = row["iti_doc_no"] == DBNull.Value ? string.Empty : row["iti_doc_no"].ToString(),
                iti_itm_cd = row["iti_itm_cd"] == DBNull.Value ? string.Empty : row["iti_itm_cd"].ToString(),
                iti_qty = row["iti_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["iti_qty"].ToString()),
            };
         }




    }
}
