using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class IMP_CST_ELEREF_DET
    {
       public Int64 iced_seq { get; set; }
       public Int32 iced_ele_line { get; set; }
       public Int32 iced_ref_line { get; set; }
       public Int32 iced_det_line { get; set; }
       public string iced_ref { get; set; }
       public string iced_cd { get; set; }
       public decimal iced_amt { get; set; }
       public Int32 iced_act { get; set; }
       public string iced_cre_by { get; set; }
       public DateTime iced_cre_dt { get; set; }
       public string iced_mod_by { get; set; }
       public DateTime iced_mod_dt { get; set; }
       public string container { get; set; }
       public decimal totval { get; set; }
       public string iced_veh_reg { get; set; }
       public static IMP_CST_ELEREF_DET Converter(DataRow row)
       {
           return new IMP_CST_ELEREF_DET
           {
               iced_seq = row["iced_seq"] == DBNull.Value ? 0 : Convert.ToInt64(row["iced_seq"]),
               iced_ele_line = row["iced_ele_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["iced_ele_line"]),
               iced_ref_line = row["iced_ref_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["iced_ref_line"]),
               iced_det_line = row["iced_det_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["iced_det_line"]),
               iced_ref = row["iced_ref"] == DBNull.Value ? string.Empty : row["iced_ref"].ToString(),
               iced_cd = row["iced_cd"] == DBNull.Value ? string.Empty : row["iced_cd"].ToString(),
               iced_amt = row["iced_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["iced_amt"]),
               iced_act = row["iced_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["iced_act"]),
               iced_cre_by = row["iced_cre_by"] == DBNull.Value ? string.Empty : row["iced_cre_by"].ToString(),
               iced_cre_dt = row["iced_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["iced_cre_dt"].ToString()),
               iced_mod_by = row["iced_mod_by"] == DBNull.Value ? string.Empty : row["iced_mod_by"].ToString(),
               iced_mod_dt = row["iced_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["iced_mod_dt"].ToString()),
               iced_veh_reg = row["iced_veh_reg"] == DBNull.Value ? string.Empty : row["iced_veh_reg"].ToString(),
           };
       }
    }
}
