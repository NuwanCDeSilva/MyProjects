using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
     [Serializable]
    public class ModelCatAndTypes
    {
        public Int32 MCT_SEQ { get; set; }
        public String MCT_MCAT_CD { get; set; }
        public String MCT_CLS_CAT { get; set; }
        public String MCT_CLS_TP { get; set; }
        public Int32 MCT_ACT { get; set; }
        public Int32 MCT_CAT_ORDR { get; set; }
        public Int32 MCT_TP_ORDR { get; set; }
        public String MCT_CRE_BY { get; set; }
        public DateTime MCT_CRE_DT { get; set; }
        public String MCT_MOD_BY { get; set; }
        public DateTime MCT_MOD_DT { get; set; }

        public String MCT_CLS_CAT_DES { get; set; }
        public String MCT_CLS_TP_DES { get; set; }
        public String MCT_DEF  { get; set; }
        public static ModelCatAndTypes Converter(DataRow row)
        {
            return new ModelCatAndTypes
            {
                MCT_SEQ = row["MCT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCT_SEQ"].ToString()),
                MCT_MCAT_CD = row["MCT_MCAT_CD"] == DBNull.Value ? string.Empty : row["MCT_MCAT_CD"].ToString(),
                MCT_CLS_CAT = row["MCT_CLS_CAT"] == DBNull.Value ? string.Empty : row["MCT_CLS_CAT"].ToString(),
                MCT_CLS_TP = row["MCT_CLS_TP"] == DBNull.Value ? string.Empty : row["MCT_CLS_TP"].ToString(),
                MCT_ACT = row["MCT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCT_ACT"].ToString()),
                MCT_CAT_ORDR = row["MCT_CAT_ORDR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCT_CAT_ORDR"].ToString()),
                MCT_TP_ORDR = row["MCT_TP_ORDR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCT_TP_ORDR"].ToString()),
                MCT_CRE_BY = row["MCT_CRE_BY"] == DBNull.Value ? string.Empty : row["MCT_CRE_BY"].ToString(),
                MCT_CRE_DT = row["MCT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCT_CRE_DT"].ToString()),
                MCT_MOD_BY = row["MCT_MOD_BY"] == DBNull.Value ? string.Empty : row["MCT_MOD_BY"].ToString(),
                MCT_MOD_DT = row["MCT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCT_MOD_DT"].ToString()),
                MCT_CLS_CAT_DES = row["mcc_cat_desc"] == DBNull.Value ? string.Empty : row["mcc_cat_desc"].ToString(),
                MCT_CLS_TP_DES = row["mcp_tp_desc"] == DBNull.Value ? string.Empty : row["mcp_tp_desc"].ToString(),              
            };
        }
    } 
}
