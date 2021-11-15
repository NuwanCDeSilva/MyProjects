using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
   public class DeliveryItemDetails
    {
       public string bms_com_cd { get; set; }
       public string bms_chnl { get; set; }
       public string bms_pc_sub_chnl { get; set; }
       public string bms_pc_area { get; set; }
       public string bms_pc_zone { get; set; }
       public string bms_pc_region { get; set; }
       public Int32 bms_d_qty { get; set; }

       public static DeliveryItemDetails Converter(DataRow row)
       {
           return new DeliveryItemDetails
           {
               bms_com_cd = row["bms_com_cd"] == DBNull.Value ? string.Empty : row["bms_com_cd"].ToString(),
               bms_chnl = row["bms_chnl"] == DBNull.Value ? string.Empty : row["bms_chnl"].ToString(),
               bms_d_qty = row["bms_d_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["bms_d_qty"].ToString()),
           };
       } 
    }

}
