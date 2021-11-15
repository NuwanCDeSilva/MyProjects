using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CHKINOUT
    {
        public Int32 CHK_SEQ { get; set; }
        public String CHK_ENQ_ID { get; set; }
        public decimal CHK_OUT_KM { get; set; }
        public DateTime CHK_OUT_DTE { get; set; }
        public decimal CHK_OUT_FUEL { get; set; }
        public DateTime CHK_IN_DTE { get; set; }
        public decimal CHK_IN_KM { get; set; }
        public decimal CHK_IN_FUEL { get; set; }
        public decimal CHK_OTH_CHRG { get; set; }
        public String CHK_RMKS { get; set; }

        public String CHK_ANAL1{ get; set; }
        public String  CHK_ANAL2{ get; set; }
        public String  CHK_ANAL3{ get; set; }
        public String  CHK_CRE_BY{ get; set; }
        public String CHK_MOD_BY { get; set; }
        public static MST_CHKINOUT Converter(DataRow row)
        {
            return new MST_CHKINOUT
            {
                CHK_SEQ = row["CHK_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_SEQ"].ToString()),
                CHK_ENQ_ID = row["CHK_ENQ_ID"] == DBNull.Value ? string.Empty : row["CHK_ENQ_ID"].ToString(),
                CHK_OUT_KM = row["CHK_OUT_KM"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_OUT_KM"].ToString()),
                CHK_OUT_DTE = row["CHK_OUT_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CHK_OUT_DTE"].ToString()),
                CHK_OUT_FUEL = row["CHK_OUT_FUEL"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_OUT_FUEL"].ToString()),
                CHK_IN_DTE = row["CHK_IN_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CHK_IN_DTE"].ToString()),
                CHK_IN_KM = row["CHK_IN_KM"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_IN_KM"].ToString()),
                CHK_IN_FUEL = row["CHK_IN_FUEL"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_IN_FUEL"].ToString()),
                CHK_OTH_CHRG = row["CHK_OTH_CHRG"] == DBNull.Value ? 0 : Convert.ToInt32(row["CHK_OTH_CHRG"].ToString()),
                CHK_RMKS = row["CHK_RMKS"] == DBNull.Value ? string.Empty : row["CHK_RMKS"].ToString(),
                CHK_ANAL1 = row["CHK_ANAL1"] == DBNull.Value ? string.Empty : row["CHK_ANAL1"].ToString(),
                CHK_ANAL2 = row["CHK_ANAL2"] == DBNull.Value ? string.Empty : row["CHK_ANAL2"].ToString(),
                CHK_ANAL3 = row["CHK_ANAL3"] == DBNull.Value ? string.Empty : row["CHK_ANAL3"].ToString(),
                CHK_CRE_BY = row["CHK_CRE_BY"] == DBNull.Value ? string.Empty : row["CHK_CRE_BY"].ToString(),
                CHK_MOD_BY = row["CHK_MOD_BY"] == DBNull.Value ? string.Empty : row["CHK_MOD_BY"].ToString()
            };
        }
    }

}
