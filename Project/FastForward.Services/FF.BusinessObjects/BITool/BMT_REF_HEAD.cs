using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BMT_REF_HEAD
    {
        public string Bmr_col_nm { get; set; }
        public string Bmr_col_desc { get; set; }
        public string Bmr_rep_name { get; set; }
        public string Bmr_data_tp { get; set; }
        public int Bmr_is_value { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static BMT_REF_HEAD Converter(DataRow row)
        {
            return new BMT_REF_HEAD
            {
                Bmr_col_nm = row["Bmr_col_nm"] == DBNull.Value ? string.Empty : row["Bmr_col_nm"].ToString(),
                Bmr_col_desc = row["Bmr_col_desc"] == DBNull.Value ? string.Empty : row["Bmr_col_desc"].ToString(),
                Bmr_rep_name = row["Bmr_rep_name"] == DBNull.Value ? string.Empty : row["Bmr_rep_name"].ToString(),
                Bmr_data_tp = row["Bmr_data_tp"] == DBNull.Value ? string.Empty : row["Bmr_data_tp"].ToString(),
                Bmr_is_value = row["Bmr_is_value"] == DBNull.Value ? 0 : Convert.ToInt32(row["Bmr_is_value"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
              
            };
        } 
    }
}
