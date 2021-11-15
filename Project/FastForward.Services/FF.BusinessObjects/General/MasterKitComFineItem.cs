using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
     [Serializable]
    public class MasterKitComFineItem
    {
        public String MIF_KIT_CD { get; set; }
        public String MIF_FG_CD { get; set; }
        public Int32 MIF_ACT { get; set; }
        public String MIF_CRE_BY { get; set; }
        public DateTime MIF_CRE_DT { get; set; }
        public String MIF_MOD_BY { get; set; }
        public DateTime MIF_MOD_DT { get; set; }
        public static MasterKitComFineItem Converter(DataRow row)
        {
            return new MasterKitComFineItem
            {
                MIF_KIT_CD = row["MIF_KIT_CD"] == DBNull.Value ? string.Empty : row["MIF_KIT_CD"].ToString(),
                MIF_FG_CD = row["MIF_FG_CD"] == DBNull.Value ? string.Empty : row["MIF_FG_CD"].ToString(),
                MIF_ACT = row["MIF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MIF_ACT"].ToString()),
                MIF_CRE_BY = row["MIF_CRE_BY"] == DBNull.Value ? string.Empty : row["MIF_CRE_BY"].ToString(),
                MIF_CRE_DT = row["MIF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIF_CRE_DT"].ToString()),
                MIF_MOD_BY = row["MIF_MOD_BY"] == DBNull.Value ? string.Empty : row["MIF_MOD_BY"].ToString(),
                MIF_MOD_DT = row["MIF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIF_MOD_DT"].ToString())
            };
        }
    }
 }

