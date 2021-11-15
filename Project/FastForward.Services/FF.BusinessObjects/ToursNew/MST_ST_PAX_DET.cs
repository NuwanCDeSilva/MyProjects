using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_ST_PAX_DET
    {
        public Int32 SPD_SEQ { get; set; }
        public String SPD_INV_NO { get; set; }
        public Int32 SPD_LINE { get; set; }
        public String SPD_ENQ_ID { get; set; }
        public String SPD_CUS_CD { get; set; }
        public String SPD_CUS_NAME { get; set; }
        public String SPD_PP_NO{ get; set; }
        public string SPD_MOB{ get; set; }
        public String SPD_NIC { get; set; }
        public String SPD_RMK { get; set; }
        public decimal SPD_AMT { get; set; }
        public Int32 SPD_STUS { get; set; }
        public static MST_ST_PAX_DET Converter(DataRow row)
        {
            return new MST_ST_PAX_DET
            {
                SPD_SEQ = row["SPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_SEQ"].ToString()),
                SPD_INV_NO = row["SPD_INV_NO"] == DBNull.Value ? string.Empty : row["SPD_INV_NO"].ToString(),
                SPD_LINE = row["SPD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_LINE"].ToString()),
                SPD_ENQ_ID = row["SPD_ENQ_ID"] == DBNull.Value ? string.Empty : row["SPD_ENQ_ID"].ToString(),
                SPD_CUS_CD = row["SPD_CUS_CD"] == DBNull.Value ? string.Empty : row["SPD_CUS_CD"].ToString(),
                SPD_CUS_NAME = row["SPD_CUS_NAME"] == DBNull.Value ? string.Empty : row["SPD_CUS_NAME"].ToString(),
                SPD_RMK = row["SPD_RMK"] == DBNull.Value ? string.Empty : row["SPD_RMK"].ToString(),
                SPD_AMT = row["SPD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPD_AMT"].ToString()),
                SPD_PP_NO = row["SPD_PP_NO"] == DBNull.Value ? string.Empty : row["SPD_PP_NO"].ToString(),
                SPD_MOB = row["SPD_MOB"] == DBNull.Value ?string.Empty :row["SPD_MOB"].ToString(),
                SPD_NIC = row["SPD_NIC"] == DBNull.Value ? string.Empty : row["SPD_NIC"].ToString(),
                SPD_STUS = row["SPD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPD_STUS"].ToString())
            };
        } 
    }
}
