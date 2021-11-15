using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class hpr_disr_val_ref
    {
        public Decimal hdvr_from_val { get; set; }
        public Decimal hdvr_to_val { get; set; }
        public Decimal hdvr_val { get; set; }
        public Int32 hdvr_tp { get; set; }
        public String hdvr_cre_by { get; set; }
        public DateTime hdvr_cre_dt { get; set; }
        public String hdvr_mod_by { get; set; }
        public DateTime hdvr_mod_dt { get; set; }
        public String hdvr_circular { get; set; }
        public String hdvr_ratevalue { get; set; }
        public String hdvr_base { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static hpr_disr_val_ref webConverter(DataRow row)
        {
            return new hpr_disr_val_ref
            {
                hdvr_from_val = row["hdvr_from_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hdvr_from_val"].ToString()),
                hdvr_to_val = row["hdvr_to_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hdvr_to_val"].ToString()),
                hdvr_val = row["hdvr_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hdvr_val"].ToString()),
                hdvr_tp = row["hdvr_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["hdvr_tp"].ToString()),
                hdvr_cre_by = row["hdvr_cre_by"] == DBNull.Value ? string.Empty : row["hdvr_cre_by"].ToString(),
                hdvr_cre_dt = row["hdvr_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdvr_cre_dt"]),
                hdvr_mod_by = row["hdvr_mod_by"] == DBNull.Value ? string.Empty : row["hdvr_mod_by"].ToString(),
                hdvr_mod_dt = row["hdvr_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdvr_mod_dt"]),
                hdvr_circular = row["hdvr_circular"] == DBNull.Value ? string.Empty : row["hdvr_circular"].ToString(),
                hdvr_ratevalue = row["hdvr_ratevalue"] == DBNull.Value ? string.Empty : row["hdvr_ratevalue"].ToString(),
                hdvr_base = row["hdvr_base"] == DBNull.Value ? string.Empty : row["hdvr_base"].ToString()
            };
        }

        public static hpr_disr_val_ref Converter(DataRow row)
        {
            return new hpr_disr_val_ref
            {

                hdvr_circular = row["hdvr_circular"] == DBNull.Value ? string.Empty : row["hdvr_circular"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
