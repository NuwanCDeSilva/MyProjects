using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MasterBufferChannel
    {
        #region Private Members
        public String MBC_COM { get; set; }
        public String MBC_CHNL { get; set; }
        public String MBC_GRADE { get; set; }
        public String MBC_SEASON { get; set; }
        public String MBC_ITM_CD { get; set; }
        public String MBC_ITM_STUS { get; set; }
        public Int32 MBC_QTY { get; set; }
        public Int32 MBC_ACT { get; set; }
        public String MBC_CRE_BY { get; set; }
        public DateTime MBC_CRE_DT { get; set; }
        public String MBC_CRE_SESSION { get; set; }
        public String MBC_MOD_BY { get; set; }
        public DateTime MBC_MOD_DT { get; set; }
        public String MBC_MOD_SESSION { get; set; }
        public decimal MBC_DEC_QTY { get; set; }//Add by lakshan 28Dec2017

        #endregion

        public static MasterBufferChannel Converter(DataRow row)  
        {
            return new MasterBufferChannel
           {
               MBC_COM = row["MBC_COM"] == DBNull.Value ? string.Empty : row["MBC_COM"].ToString(),
               MBC_CHNL = row["MBC_CHNL"] == DBNull.Value ? string.Empty : row["MBC_CHNL"].ToString(),
               MBC_GRADE = row["MBC_GRADE"] == DBNull.Value ? string.Empty : row["MBC_GRADE"].ToString(),
               MBC_SEASON = row["MBC_SEASON"] == DBNull.Value ? string.Empty : row["MBC_SEASON"].ToString(),
               MBC_ITM_CD = row["MBC_ITM_CD"] == DBNull.Value ? string.Empty : row["MBC_ITM_CD"].ToString(),
               MBC_ITM_STUS = row["MBC_ITM_STUS"] == DBNull.Value ? string.Empty : row["MBC_ITM_STUS"].ToString(),
               MBC_QTY = row["MBC_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBC_QTY"].ToString()),
               MBC_ACT = row["MBC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBC_ACT"].ToString()),
               MBC_CRE_BY = row["MBC_CRE_BY"] == DBNull.Value ? string.Empty : row["MBC_CRE_BY"].ToString(),
               MBC_CRE_DT = row["MBC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBC_CRE_DT"].ToString()),
               MBC_CRE_SESSION = row["MBC_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MBC_CRE_SESSION"].ToString(),
               MBC_MOD_BY = row["MBC_MOD_BY"] == DBNull.Value ? string.Empty : row["MBC_MOD_BY"].ToString(),
               MBC_MOD_DT = row["MBC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBC_MOD_DT"].ToString()),
               MBC_MOD_SESSION = row["MBC_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MBC_MOD_SESSION"].ToString()
           };
        
        
        } 
    
        
    }
}
