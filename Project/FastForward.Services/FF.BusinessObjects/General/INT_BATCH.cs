using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.General
{
    public class INT_BATCH
    {
        public Int32 ITB_SEQ_NO { get; set; }
        public Int32 ITB_ITM_LINE { get; set; }
        public Int32 ITB_BATCH_LINE { get; set; }
        public String ITB_BATCH_NO { get; set; }
        public String ITB_DOC_NO { get; set; }
        public String ITB_COM { get; set; }
        public String ITB_LOC { get; set; }
        public String ITB_BIN { get; set; }
        public String ITB_ITM_CD { get; set; }
        public String ITB_ITM_STUS { get; set; }
        public Decimal ITB_QTY { get; set; }
        public Decimal ITB_UNIT_COST { get; set; }
        public Decimal ITB_BAL_QTY1 { get; set; }
        public Decimal ITB_BAL_QTY2 { get; set; }
        public Decimal ITB_UNIT_PRICE { get; set; }
        public String ITB_BASE_DOC_NO { get; set; }
        public String ITB_BASE_REF_NO { get; set; }
        public DateTime ITB_BASE_DOC_DT { get; set; }
        public String ITB_BASE_ITMCD { get; set; }
        public Int32 ITB_BASE_ITMLINE { get; set; }
        public String ITB_BASE_ITMSTUS { get; set; }
        public String ITB_JOB_NO { get; set; }
        public Int32 ITB_GIT_IGNORE { get; set; }
        public String ITB_GIT_IGNORE_BY { get; set; }
        public DateTime ITB_GIT_IGNORE_DT { get; set; }
        public DateTime ITB_GIT_IGNORE_EFFDT { get; set; }
        public String ITB_RES_NO { get; set; }
        public Int32 ITB_RES_LINENO { get; set; }
        public String ITB_BASE_DOC_NO1 { get; set; }
        public String ITB_BASE_DOC_NO2 { get; set; }
        public String ITB_BASE_DOC_NO3 { get; set; }
        public String ITB_CUR_CD { get; set; }
        public String ITB_GRUP_CUR_CD { get; set; }
        public Int32 ITB_BASE_BATCHLINE { get; set; }
        public Int32 ITB_BASE_REFLINE { get; set; }
        public Int32 ITB_JOB_LINE { get; set; }
        public DateTime ITB_EXP_DT { get; set; }
        public DateTime ITB_MANUFAC_DT { get; set; }
        public String ITB_MITM_CD { get; set; }
        public static INT_BATCH Converter(DataRow row)
        {
            return new INT_BATCH
            {
                ITB_SEQ_NO = row["ITB_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_SEQ_NO"].ToString()),
                ITB_ITM_LINE = row["ITB_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_ITM_LINE"].ToString()),
                ITB_BATCH_LINE = row["ITB_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BATCH_LINE"].ToString()),
                ITB_BATCH_NO = row["ITB_BATCH_NO"] == DBNull.Value ? string.Empty : row["ITB_BATCH_NO"].ToString(),
                ITB_DOC_NO = row["ITB_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_DOC_NO"].ToString(),
                ITB_COM = row["ITB_COM"] == DBNull.Value ? string.Empty : row["ITB_COM"].ToString(),
                ITB_LOC = row["ITB_LOC"] == DBNull.Value ? string.Empty : row["ITB_LOC"].ToString(),
                ITB_BIN = row["ITB_BIN"] == DBNull.Value ? string.Empty : row["ITB_BIN"].ToString(),
                ITB_ITM_CD = row["ITB_ITM_CD"] == DBNull.Value ? string.Empty : row["ITB_ITM_CD"].ToString(),
                ITB_ITM_STUS = row["ITB_ITM_STUS"] == DBNull.Value ? string.Empty : row["ITB_ITM_STUS"].ToString(),
                ITB_QTY = row["ITB_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_QTY"].ToString()),
                ITB_UNIT_COST = row["ITB_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_COST"].ToString()),
                ITB_BAL_QTY1 = row["ITB_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY1"].ToString()),
                ITB_BAL_QTY2 = row["ITB_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_BAL_QTY2"].ToString()),
                ITB_UNIT_PRICE = row["ITB_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITB_UNIT_PRICE"].ToString()),
                ITB_BASE_DOC_NO = row["ITB_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO"].ToString(),
                ITB_BASE_REF_NO = row["ITB_BASE_REF_NO"] == DBNull.Value ? string.Empty : row["ITB_BASE_REF_NO"].ToString(),
                ITB_BASE_DOC_DT = row["ITB_BASE_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_BASE_DOC_DT"].ToString()),
                ITB_BASE_ITMCD = row["ITB_BASE_ITMCD"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMCD"].ToString(),
                ITB_BASE_ITMLINE = row["ITB_BASE_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_ITMLINE"].ToString()),
                ITB_BASE_ITMSTUS = row["ITB_BASE_ITMSTUS"] == DBNull.Value ? string.Empty : row["ITB_BASE_ITMSTUS"].ToString(),
                ITB_JOB_NO = row["ITB_JOB_NO"] == DBNull.Value ? string.Empty : row["ITB_JOB_NO"].ToString(),
                ITB_GIT_IGNORE = row["ITB_GIT_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_GIT_IGNORE"].ToString()),
                ITB_GIT_IGNORE_BY = row["ITB_GIT_IGNORE_BY"] == DBNull.Value ? string.Empty : row["ITB_GIT_IGNORE_BY"].ToString(),
                ITB_GIT_IGNORE_DT = row["ITB_GIT_IGNORE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_DT"].ToString()),
                ITB_GIT_IGNORE_EFFDT = row["ITB_GIT_IGNORE_EFFDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_GIT_IGNORE_EFFDT"].ToString()),
                ITB_RES_NO = row["ITB_RES_NO"] == DBNull.Value ? string.Empty : row["ITB_RES_NO"].ToString(),
                ITB_RES_LINENO = row["ITB_RES_LINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_RES_LINENO"].ToString()),
                ITB_BASE_DOC_NO1 = row["ITB_BASE_DOC_NO1"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO1"].ToString(),
                ITB_BASE_DOC_NO2 = row["ITB_BASE_DOC_NO2"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO2"].ToString(),
                ITB_BASE_DOC_NO3 = row["ITB_BASE_DOC_NO3"] == DBNull.Value ? string.Empty : row["ITB_BASE_DOC_NO3"].ToString(),
                ITB_CUR_CD = row["ITB_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_CUR_CD"].ToString(),
                ITB_GRUP_CUR_CD = row["ITB_GRUP_CUR_CD"] == DBNull.Value ? string.Empty : row["ITB_GRUP_CUR_CD"].ToString(),
                ITB_BASE_BATCHLINE = row["ITB_BASE_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_BATCHLINE"].ToString()),
                ITB_BASE_REFLINE = row["ITB_BASE_REFLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_BASE_REFLINE"].ToString()),
                ITB_JOB_LINE = row["ITB_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITB_JOB_LINE"].ToString()),
                ITB_EXP_DT = row["ITB_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_EXP_DT"].ToString()),
                ITB_MANUFAC_DT = row["ITB_MANUFAC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITB_MANUFAC_DT"].ToString()),
                ITB_MITM_CD = row["ITB_MITM_CD"] == DBNull.Value ? string.Empty : row["ITB_MITM_CD"].ToString()
            };
        }
    }

}
