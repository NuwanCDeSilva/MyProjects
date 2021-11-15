using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  public  class RatioData
    {
      public string itb_itm_cd { get; set; }
      public string mi_cate_1 { get; set; }
      public string mi_brand { get; set; }
      public string mi_model { get; set; }
      public Int32 Qty { get; set; }
      public decimal Years { get; set; }
      public Int32 ImportQty { get; set; }
      public Int32 NetSaleQty { get; set; }
      public Int32 DefectiveQty { get; set; }
      public double DefectiveRate { get; set; }
      public Int32 Totalldefqty { get; set; }

      public static RatioData Converter(DataRow row)
        {
            return new RatioData
            {
                itb_itm_cd = row["itb_itm_cd"] == DBNull.Value ? string.Empty : row["itb_itm_cd"].ToString(),
                mi_cate_1 = row["mi_cate_1"] == DBNull.Value ? string.Empty : row["mi_cate_1"].ToString(),
                mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString(),
                mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                Qty = row["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["Qty"].ToString()),
                Years = row["Years"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Years"].ToString()),
            };
         }
    }

}
