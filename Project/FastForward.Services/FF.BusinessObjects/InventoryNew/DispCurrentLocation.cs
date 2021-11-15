using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class DispCurrentLocation
    {
        public Int32 Idc_seq { get; set; }
        public String Idc_doc_no { get; set; }
        public String Idc_loc { get; set; }
        public Int32 Idc_act { get; set; }
        public String Idc_cre_by { get; set; }
        public DateTime Idc_cre_dt { get; set; }
        public String Idc_mod_by { get; set; }
        public DateTime Idc_mod_dt { get; set; }

        //Additional
        public String Idc_loc_Desc { get; set; }

        public static DispCurrentLocation Converter(DataRow row)
        {
            return new DispCurrentLocation
            {
                Idc_seq = row["IDC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDC_SEQ"].ToString()),
                Idc_doc_no = row["IDC_DOC_NO"] == DBNull.Value ? string.Empty : row["IDC_DOC_NO"].ToString(),
                Idc_loc = row["IDC_LOC"] == DBNull.Value ? string.Empty : row["IDC_LOC"].ToString(),
                Idc_act = row["IDC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDC_ACT"].ToString()),
                Idc_cre_by = row["IDC_CRE_BY"] == DBNull.Value ? string.Empty : row["IDC_CRE_BY"].ToString(),
                Idc_cre_dt = row["IDC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDC_CRE_DT"].ToString()),
                Idc_mod_by = row["IDC_MOD_BY"] == DBNull.Value ? string.Empty : row["IDC_MOD_BY"].ToString(),
                Idc_mod_dt = row["IDC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDC_MOD_DT"].ToString()),
                Idc_loc_Desc = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString()
            };
        }
    }
}
