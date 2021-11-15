using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class ModelPic
    {
        public String MMP_MODEL { get; set; }
        public Int32 MMP_LINE { get; set; }
        public String MMP_TP { get; set; }
        public String MMP_PATH { get; set; }
        public Int32 MMP_ACT { get; set; }
        public String MMP_CRE_BY { get; set; }
        public DateTime MMP_CRE_DT { get; set; }
        public String MMP_MOD_BY { get; set; }
        public DateTime MMP_MOD_DT { get; set; }
        public String MMP_RMK { get; set; }
        public static ModelPic Converter(DataRow row)
        {
            return new ModelPic
            {
                MMP_MODEL = row["MMP_MODEL"] == DBNull.Value ? string.Empty : row["MMP_MODEL"].ToString(),
                MMP_LINE = row["MMP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMP_LINE"].ToString()),
                MMP_TP = row["MMP_TP"] == DBNull.Value ? string.Empty : row["MMP_TP"].ToString(),
                MMP_PATH = row["MMP_PATH"] == DBNull.Value ? string.Empty : row["MMP_PATH"].ToString(),
                MMP_ACT = row["MMP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMP_ACT"].ToString()),
                MMP_CRE_BY = row["MMP_CRE_BY"] == DBNull.Value ? string.Empty : row["MMP_CRE_BY"].ToString(),
                MMP_CRE_DT = row["MMP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMP_CRE_DT"].ToString()),
                MMP_MOD_BY = row["MMP_MOD_BY"] == DBNull.Value ? string.Empty : row["MMP_MOD_BY"].ToString(),
                MMP_MOD_DT = row["MMP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMP_MOD_DT"].ToString()),
                MMP_RMK = row["MMP_RMK"] == DBNull.Value ? string.Empty : row["MMP_RMK"].ToString()
            };
        }
    }
}
