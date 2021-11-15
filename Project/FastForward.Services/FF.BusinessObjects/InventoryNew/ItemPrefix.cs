using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
       [Serializable]
    public class ItemPrefix
    {   
        #region Private Members
        public String MIP_COM { get; set; }
        public String MIP_CD { get; set; }
        public String MI_PREFIX { get; set; }
        public String MI_DESC { get; set; }
        public Int32 MI_ACT { get; set; }
        public String MI_CRE_BY { get; set; }
        public DateTime MI_CRE_DT { get; set; }
        public String MI_CRE_SESSION { get; set; }
        public String MI_MOD_BY { get; set; }
        public DateTime MI_MOD_DT { get; set; }
        public String MI_MOD_SESSION { get; set; }

        public String MI_ACTIVE_STATUS { get; set; }
        #endregion

        public static ItemPrefix Converter(DataRow row)
        {
            return new ItemPrefix
            {
                MIP_COM = row["MIP_COM"] == DBNull.Value ? string.Empty : row["MIP_COM"].ToString(),
                MIP_CD = row["MIP_CD"] == DBNull.Value ? string.Empty : row["MIP_CD"].ToString(),
                MI_PREFIX = row["MI_PREFIX"] == DBNull.Value ? string.Empty : row["MI_PREFIX"].ToString(),
                MI_DESC = row["MI_DESC"] == DBNull.Value ? string.Empty : row["MI_DESC"].ToString(),
                MI_ACT = row["MI_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MI_ACT"].ToString()),
                MI_CRE_BY = row["MI_CRE_BY"] == DBNull.Value ? string.Empty : row["MI_CRE_BY"].ToString(),
                MI_CRE_DT = row["MI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_CRE_DT"].ToString()),
                MI_CRE_SESSION = row["MI_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MI_CRE_SESSION"].ToString(),
                MI_MOD_BY = row["MI_MOD_BY"] == DBNull.Value ? string.Empty : row["MI_MOD_BY"].ToString(),
                MI_MOD_DT = row["MI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_MOD_DT"].ToString()),
                MI_MOD_SESSION = row["MI_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MI_MOD_SESSION"].ToString()
            };
        }
    }
   }

