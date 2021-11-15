using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class SerJobDesc
    {
       public string sd_desc { get; set; }
       public string jbd_itm_cd { get; set; }
       public Int32 Qty { get; set; }
               public static SerJobDesc Converter(DataRow row)
        {
            return new SerJobDesc
            {
                sd_desc = row["sd_desc"] == DBNull.Value ? string.Empty : row["sd_desc"].ToString(),
                jbd_itm_cd = row["jbd_itm_cd"] == DBNull.Value ? string.Empty : row["jbd_itm_cd"].ToString(),
                Qty = row["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["Qty"].ToString())
            };
               }
    }
}
