using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
  public  class UnitConvert
    {
        public string mmu_com { get; set; }
        public string mmu_model { get; set; }
        public string mmu_model_uom { get; set; }
        public string mmu_con_uom { get; set; }
        public decimal mmu_qty { get; set; }
        public Int32 mmu_act { get; set; }
        public string mmu_cre_by { get; set; }
        public DateTime mmu_cre_dt { get; set; }
        public string mmu_mod_by { get; set; }
        public DateTime mmu_mod_dt { get; set; }


        public static UnitConvert Converter(DataRow row)
        {
            return new UnitConvert
            {
                mmu_com = row["mmu_com"] == DBNull.Value ? string.Empty : row["mmu_com"].ToString(),
                mmu_model = row["mmu_model"] == DBNull.Value ? string.Empty : row["mmu_model"].ToString(),
                mmu_model_uom = row["mmu_model_uom"] == DBNull.Value ? string.Empty : row["mmu_model_uom"].ToString(),
                mmu_con_uom = row["mmu_con_uom"] == DBNull.Value ? string.Empty : row["mmu_con_uom"].ToString(),
                mmu_qty = row["mmu_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mmu_qty"].ToString()),
                mmu_act = row["mmu_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["mmu_act"].ToString()),
                mmu_cre_by = row["mmu_cre_by"] == DBNull.Value ? string.Empty : row["mmu_cre_by"].ToString(),
                mmu_cre_dt = row["mmu_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mmu_cre_dt"]),
                mmu_mod_by = row["mmu_mod_by"] == DBNull.Value ? string.Empty : row["mmu_mod_by"].ToString(),
                mmu_mod_dt = row["mmu_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mmu_mod_dt"]),
   
            };
        }
    }
}
