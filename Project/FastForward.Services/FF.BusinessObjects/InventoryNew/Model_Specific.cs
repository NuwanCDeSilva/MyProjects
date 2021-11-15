using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
   public class Model_Specific
    {
       public string mcd_model_cd { get; set; }
       public string mcd_cls_cat { get; set; }
       public string mcd_cls_tp { get; set; }
       public string mcd_def { get; set; }
       public string mmp_tp { get; set; }
       public string mmp_path { get; set; }
       public string ItemCode { get; set; }
       public string Cat1 { get; set; }
       public string Cat2 { get; set; }
       public static Model_Specific Converter(DataRow row)
       {
           return new Model_Specific
           {
               mcd_model_cd = row["mcd_model_cd"] == DBNull.Value ? string.Empty : row["mcd_model_cd"].ToString(),
               mcd_cls_cat = row["mcd_cls_cat"] == DBNull.Value ? string.Empty : row["mcd_cls_cat"].ToString(),
               mcd_cls_tp = row["mcd_cls_tp"] == DBNull.Value ? string.Empty : row["mcd_cls_tp"].ToString(),
               mcd_def = row["mcd_def"] == DBNull.Value ? string.Empty : row["mcd_def"].ToString(),
               mmp_tp = row["mmp_tp"] == DBNull.Value ? string.Empty : row["mmp_tp"].ToString(),
               mmp_path = row["mmp_path"] == DBNull.Value ? string.Empty : row["mmp_path"].ToString(),
               ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : row["ItemCode"].ToString(),
               Cat1 = row["Cat1"] == DBNull.Value ? string.Empty : row["Cat1"].ToString(),
               Cat2 = row["Cat2"] == DBNull.Value ? string.Empty : row["Cat2"].ToString(),
           };
       }
    }
}
