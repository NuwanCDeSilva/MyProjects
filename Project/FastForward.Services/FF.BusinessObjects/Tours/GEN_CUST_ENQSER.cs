using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
    [Serializable]
    public class GEN_CUST_ENQSER
    {
        public Int32 GCS_SEQ { get; set; }
        public Int32 GCS_LINE { get; set; }
        public String GCS_MAIN_ID { get; set; }
        public String GCS_ENQ_ID { get; set; }
        public String GCS_FAC { get; set; }
        public String GCS_SERVICE { get; set; }
        public Int32 GCS_UNITS { get; set; }
        public String GCS_SER_PROVIDER { get; set; }
        public String GCS_SER_COM { get; set; }
        public String GCS_SER_PC { get; set; }
        public String GCS_PICK_FRM { get; set; }
        public String GCS_PICK_TN { get; set; }
        public DateTime GCS_EXP_DT { get; set; }
        public DateTime GCS_EXP_TIME { get; set; }
        public String GCS_DROP { get; set; }
        public String GCS_DROP_TN { get; set; }
        public DateTime GCS_DROP_DT { get; set; }
        public DateTime GCS_DROP_TIME { get; set; }
        public String GCS_VEH_TP { get; set; }
        public String GCS_COMMENT { get; set; }
        public Int32 GCS_STATUS { get; set; }
        public String GCS_CRE_BY { get; set; }
        public DateTime GCS_CRE_DT { get; set; }
        public String GCS_MOD_BY { get; set; }
        public DateTime GCS_MOD_DT { get; set; }
      //  public string GCS_CUS_TYPE { get; set; }
        public static GEN_CUST_ENQSER Converter(DataRow row)  
        { 
            return new GEN_CUST_ENQSER 
            { 
            GCS_SEQ = row["GCS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCS_SEQ"].ToString()), 
            GCS_LINE = row["GCS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCS_LINE"].ToString()), 
            GCS_MAIN_ID = row["GCS_MAIN_ID"] == DBNull.Value ? string.Empty : row["GCS_MAIN_ID"].ToString(), 
            GCS_ENQ_ID = row["GCS_ENQ_ID"] == DBNull.Value ? string.Empty : row["GCS_ENQ_ID"].ToString(), 
            GCS_FAC = row["GCS_FAC"] == DBNull.Value ? string.Empty : row["GCS_FAC"].ToString(), 
            GCS_SERVICE = row["GCS_SERVICE"] == DBNull.Value ? string.Empty : row["GCS_SERVICE"].ToString(), 
            GCS_UNITS = row["GCS_UNITS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCS_UNITS"].ToString()), 
            GCS_SER_PROVIDER = row["GCS_SER_PROVIDER"] == DBNull.Value ? string.Empty : row["GCS_SER_PROVIDER"].ToString(), 
            GCS_SER_COM = row["GCS_SER_COM"] == DBNull.Value ? string.Empty : row["GCS_SER_COM"].ToString(), 
            GCS_SER_PC = row["GCS_SER_PC"] == DBNull.Value ? string.Empty : row["GCS_SER_PC"].ToString(), 
            GCS_PICK_FRM = row["GCS_PICK_FRM"] == DBNull.Value ? string.Empty : row["GCS_PICK_FRM"].ToString(), 
            GCS_PICK_TN = row["GCS_PICK_TN"] == DBNull.Value ? string.Empty : row["GCS_PICK_TN"].ToString(), 
            GCS_EXP_DT = row["GCS_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_EXP_DT"].ToString()), 
            GCS_EXP_TIME = row["GCS_EXP_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_EXP_TIME"].ToString()), 
            GCS_DROP = row["GCS_DROP"] == DBNull.Value ? string.Empty : row["GCS_DROP"].ToString(), 
            GCS_DROP_TN = row["GCS_DROP_TN"] == DBNull.Value ? string.Empty : row["GCS_DROP_TN"].ToString(), 
            GCS_DROP_DT = row["GCS_DROP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_DROP_DT"].ToString()), 
            GCS_DROP_TIME = row["GCS_DROP_TIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_DROP_TIME"].ToString()), 
            GCS_VEH_TP = row["GCS_VEH_TP"] == DBNull.Value ? string.Empty : row["GCS_VEH_TP"].ToString(), 
            GCS_COMMENT = row["GCS_COMMENT"] == DBNull.Value ? string.Empty : row["GCS_COMMENT"].ToString(), 
            GCS_STATUS = row["GCS_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GCS_STATUS"].ToString()), 
            GCS_CRE_BY = row["GCS_CRE_BY"] == DBNull.Value ? string.Empty : row["GCS_CRE_BY"].ToString(), 
            GCS_CRE_DT = row["GCS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_CRE_DT"].ToString()), 
            GCS_MOD_BY = row["GCS_MOD_BY"] == DBNull.Value ? string.Empty : row["GCS_MOD_BY"].ToString(), 
            GCS_MOD_DT = row["GCS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GCS_MOD_DT"].ToString()),
           // GCS_CUS_TYPE = row["GCS_CUS_TYPE"] == DBNull.Value ? string.Empty : row["GCS_CUS_TYPE"].ToString()
           }; 
            } 
        } 
    }

