using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
  public  class CatwithItems
    {
        public string mi_cd { get; set; }
        public string mi_cate_2 { get; set; }
        public string mi_cate_3 { get; set; }

        public static CatwithItems Converter(DataRow row)
        {
            return new CatwithItems
            {
                mi_cd = row["mi_cd"] == DBNull.Value ? string.Empty : row["mi_cd"].ToString(),
                mi_cate_2 = row["mi_cate_2"] == DBNull.Value ? string.Empty : row["mi_cate_2"].ToString(),
                mi_cate_3 = row["mi_cate_3"] == DBNull.Value ? string.Empty : row["mi_cate_3"].ToString(),
            };
        }
    }
}
