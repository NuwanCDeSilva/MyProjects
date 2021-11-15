using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class PhysicalStockVerificationHdr
    {

        public Int32 AUSH_SEQ { get; set; }
        public String AUSH_COM { get; set; }
        public String AUSH_JOB { get; set; }
        public String AUSH_LOC { get; set; }
        public DateTime AUSH_DT { get; set; }
        public String AUSH_REM { get; set; }
        public DateTime AUSH_FRM_DT { get; set; }
        public DateTime AUSH_TO_DT { get; set; }
        public String AUSH_STUS { get; set; }
        public String AUSH_CRE_BY { get; set; }
        public DateTime AUSH_CRE_DT { get; set; }
        public String AUSH_MOD_BY { get; set; }
        public DateTime AUSH_MOD_DT { get; set; }

        public Int32 AUSH_NO_JOB { get; set; }

        public string AUSH_SUPVIS_BY { get; set; } //By Akila 2017/02/16
        public bool IsNewJob { get; set; }
        public Int32 AUSH_RECORD_COUNT { get; set; }
        public string AUSH_SESSION_ID { get; set; }
        //Akila 2017/04/28
        public string AUSH_DIPARTMENT { get; set; }
        public string AUSH_REASON { get; set; }
        public string AUSH_REMARK { get; set; }
        public Int32 AUSH_CHARGES_APP { get; set; }
        public Int32 AUSH_ADJ_REQ_IS_SEND { get; set; }

     

        public static PhysicalStockVerificationHdr Converter(DataRow row)
        {
            return new PhysicalStockVerificationHdr
            {
                AUSH_SEQ = row["AUSH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSH_SEQ"].ToString()),
                AUSH_COM = row["AUSH_COM"] == DBNull.Value ? string.Empty : row["AUSH_COM"].ToString(),
                AUSH_JOB = row["AUSH_JOB"] == DBNull.Value ? string.Empty : row["AUSH_JOB"].ToString(),
                AUSH_LOC = row["AUSH_LOC"] == DBNull.Value ? string.Empty : row["AUSH_LOC"].ToString(),
                AUSH_DT = row["AUSH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSH_DT"].ToString()),
                AUSH_REM = row["AUSH_REM"] == DBNull.Value ? string.Empty : row["AUSH_REM"].ToString(),
                AUSH_FRM_DT = row["AUSH_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSH_FRM_DT"].ToString()),
                AUSH_TO_DT = row["AUSH_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSH_TO_DT"].ToString()),
                AUSH_STUS = row["AUSH_STUS"] == DBNull.Value ? string.Empty : row["AUSH_STUS"].ToString(),
                AUSH_CRE_BY = row["AUSH_CRE_BY"] == DBNull.Value ? string.Empty : row["AUSH_CRE_BY"].ToString(),
                AUSH_CRE_DT = row["AUSH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSH_CRE_DT"].ToString()),
                AUSH_MOD_BY = row["AUSH_MOD_BY"] == DBNull.Value ? string.Empty : row["AUSH_MOD_BY"].ToString(),
                AUSH_MOD_DT = row["AUSH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUSH_MOD_DT"].ToString()),
                AUSH_SUPVIS_BY = row["AUSH_SUPVIS_BY"] == DBNull.Value ? string.Empty : row["AUSH_SUPVIS_BY"].ToString(),
                AUSH_CHARGES_APP = row["AUSH_CHARGES_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSH_CHARGES_APP"].ToString()),
                AUSH_ADJ_REQ_IS_SEND = row["AUSH_ADJ_REQ_IS_SEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUSH_ADJ_REQ_IS_SEND"].ToString())

            };
        }
    }
}

