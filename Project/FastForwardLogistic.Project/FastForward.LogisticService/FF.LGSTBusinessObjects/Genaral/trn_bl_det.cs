using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
  public  class trn_bl_det
    {
      public string bld_seq_no { get; set; }
      public Int32 bld_line { get; set; }
      public string bld_mark_nos { get; set; }
      public decimal bld_package_nos { get; set; }
      public string bld_desc_goods { get; set; }
      public decimal bld_grs_weight { get; set; }
      public string bld_grs_weight_uom { get; set; }
      public decimal bld_net_weight { get; set; }
      public string bld_net_weight_uom { get; set; }
      public decimal bld_measure { get; set; }
      public string bld_measure_uom { get; set; }
      public string bld_package_tp { get; set; }

      public static trn_bl_det Converter(DataRow row)
      {
          return new trn_bl_det
          {
              bld_seq_no = row["bld_seq_no"] == DBNull.Value ? string.Empty : row["bld_seq_no"].ToString(),

              bld_line = row["bld_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["bld_line"].ToString()),

              bld_mark_nos = row["bld_mark_nos"] == DBNull.Value ? string.Empty : row["bld_mark_nos"].ToString(),

              bld_package_nos = row["bld_package_nos"] == DBNull.Value ? 0 : Convert.ToDecimal(row["bld_package_nos"].ToString()),

              bld_desc_goods = row["bld_desc_goods"] == DBNull.Value ? string.Empty : row["bld_desc_goods"].ToString(),

              bld_grs_weight = row["bld_grs_weight"] == DBNull.Value ? 0 : Convert.ToDecimal(row["bld_grs_weight"].ToString()),

              bld_grs_weight_uom = row["bld_grs_weight_uom"] == DBNull.Value ? string.Empty : row["bld_grs_weight_uom"].ToString(),

              bld_net_weight = row["bld_net_weight"] == DBNull.Value ? 0 : Convert.ToDecimal(row["bld_net_weight"].ToString()),

              bld_net_weight_uom = row["bld_net_weight_uom"] == DBNull.Value ? string.Empty : row["bld_net_weight_uom"].ToString(),

              bld_measure = row["bld_measure"] == DBNull.Value ? 0 : Convert.ToDecimal(row["bld_measure"].ToString()),

              bld_measure_uom = row["bld_measure_uom"] == DBNull.Value ? string.Empty : row["bld_measure_uom"].ToString(),
              bld_package_tp = row["bld_package_tp"] == DBNull.Value ? string.Empty : row["bld_package_tp"].ToString(),
          };
      }
    }
}
