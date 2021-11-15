using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class ModelClsDef
    {
        public String MCD_MODEL_CD { get; set; }
        public String MCD_CLS_CAT { get; set; }
        public String MCD_CLS_TP { get; set; }
        public String MCD_DEF { get; set; }
        public Int32 MCD_CAT_ORD { get; set; }
        public Int32 MCD_TP_ORD { get; set; }
        public String MCD_CRE_BY { get; set; }
        public DateTime MCD_CRE_DT { get; set; }
        public String MCD_MOD_BY { get; set; }
        public DateTime MCD_MOD_DT { get; set; }
        public static ModelClsDef Converter(DataRow row)
        {
            return new ModelClsDef
            {
                MCD_MODEL_CD = row["MCD_MODEL_CD"] == DBNull.Value ? string.Empty : row["MCD_MODEL_CD"].ToString(),
                MCD_CLS_CAT = row["MCD_CLS_CAT"] == DBNull.Value ? string.Empty : row["MCD_CLS_CAT"].ToString(),
                MCD_CLS_TP = row["MCD_CLS_TP"] == DBNull.Value ? string.Empty : row["MCD_CLS_TP"].ToString(),
                MCD_DEF = row["MCD_DEF"] == DBNull.Value ? string.Empty : row["MCD_DEF"].ToString(),
                MCD_CAT_ORD = row["MCD_CAT_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCD_CAT_ORD"].ToString()),
                MCD_TP_ORD = row["MCD_TP_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCD_TP_ORD"].ToString()),
                MCD_CRE_BY = row["MCD_CRE_BY"] == DBNull.Value ? string.Empty : row["MCD_CRE_BY"].ToString(),
                MCD_CRE_DT = row["MCD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCD_CRE_DT"].ToString()),
                MCD_MOD_BY = row["MCD_MOD_BY"] == DBNull.Value ? string.Empty : row["MCD_MOD_BY"].ToString(),
                MCD_MOD_DT = row["MCD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCD_MOD_DT"].ToString())
            };
        }
    }
}

