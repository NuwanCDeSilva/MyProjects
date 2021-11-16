using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class TRN_JOB_COST
    {
        public String TJC_JOB_NO { get; set; }
        public Int32 TJC_LINE_NO { get; set; }
        public String TJC_REQ_NO { get; set; }
        public String TJC_REF_NO { get; set; }
        public DateTime TJC_DT { get; set; }
        public String TJC_ELEMENT_TYPE { get; set; }
        public String TJC_ELEMENT_CODE { get; set; }
        public String TJC_DESC { get; set; }
        public decimal TJC_COST_AMT { get; set; }
        public decimal TJC_REV_AMT { get; set; }
        public Int32 IS_PROVISION { get; set; }
        public String TJC_COM { get; set; }
        public String TJC_RMK { get; set; }
        public String TJC_SESSION_ID { get; set; }
        public String TJC_CRE_BY { get; set; }
        public String TJC_MOD_BY { get; set; }
        public DateTime TJC_CRE_DT { get; set; }
        public DateTime TJC_MOD_DT { get; set; }
        public Int32 TJC_ACT { get; set; }
        public decimal TJC_MARGIN { get; set; }
        public Int32 TJC_APP_1 { get; set; }
        public Int32 TJC_APP_2 { get; set; }
        public static TRN_JOB_COST Converter(DataRow row)
        {
            return new TRN_JOB_COST
            {
                TJC_JOB_NO = row["TJC_JOB_NO"] == DBNull.Value ? string.Empty : row["TJC_JOB_NO"].ToString(),
                TJC_LINE_NO = row["TJC_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_LINE_NO"].ToString()),
                TJC_REQ_NO = row["TJC_REQ_NO"] == DBNull.Value ? string.Empty : row["TJC_REQ_NO"].ToString(),
                TJC_REF_NO = row["TJC_REF_NO"] == DBNull.Value ? string.Empty : row["TJC_REF_NO"].ToString(),
                TJC_DT = row["TJC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TJC_DT"].ToString()),
                TJC_ELEMENT_TYPE = row["TJC_ELEMENT_TYPE"] == DBNull.Value ? string.Empty : row["TJC_ELEMENT_TYPE"].ToString(),
                TJC_ELEMENT_CODE = row["TJC_ELEMENT_CODE"] == DBNull.Value ? string.Empty : row["TJC_ELEMENT_CODE"].ToString(),
                TJC_DESC = row["TJC_DESC"] == DBNull.Value ? string.Empty : row["TJC_DESC"].ToString(),
                TJC_COST_AMT = row["TJC_COST_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_COST_AMT"].ToString()),
                TJC_REV_AMT = row["TJC_REV_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_REV_AMT"].ToString()),
                IS_PROVISION = row["IS_PROVISION"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_PROVISION"].ToString()),
                TJC_COM = row["TJC_COM"] == DBNull.Value ? string.Empty : row["TJC_COM"].ToString(),
                TJC_RMK = row["TJC_RMK"] == DBNull.Value ? string.Empty : row["TJC_RMK"].ToString(),
                TJC_SESSION_ID = row["TJC_SESSION_ID"] == DBNull.Value ? string.Empty : row["TJC_SESSION_ID"].ToString(),
                TJC_CRE_BY = row["TJC_CRE_BY"] == DBNull.Value ? string.Empty : row["TJC_CRE_BY"].ToString(),
                TJC_MOD_BY = row["TJC_MOD_BY"] == DBNull.Value ? string.Empty : row["TJC_MOD_BY"].ToString(),
                TJC_CRE_DT = row["TJC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TJC_CRE_DT"].ToString()),
                TJC_MOD_DT = row["TJC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TJC_MOD_DT"].ToString()),
                TJC_ACT = row["TJC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_ACT"].ToString())
            };
        }

        public static TRN_JOB_COST ConverterJobCost(DataRow row)
        {
            return new TRN_JOB_COST
            {
                TJC_JOB_NO = row["TJC_JOB_NO"] == DBNull.Value ? string.Empty : row["TJC_JOB_NO"].ToString(),
                TJC_ELEMENT_CODE = row["TJC_ELEMENT_CODE"] == DBNull.Value ? string.Empty : row["TJC_ELEMENT_CODE"].ToString(),
                TJC_DESC = row["TJC_DESC"] == DBNull.Value ? string.Empty : row["TJC_DESC"].ToString(),
                TJC_COST_AMT = row["TJC_COST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TJC_COST_AMT"].ToString())
            };
        }
        public static TRN_JOB_COST ConverterJobPreCost(DataRow row)
        {
            return new TRN_JOB_COST
            {
                TJC_JOB_NO = row["TJC_JOB_NO"] == DBNull.Value ? string.Empty : row["TJC_JOB_NO"].ToString(),
                TJC_ELEMENT_CODE = row["TJC_ELEMENT_CODE"] == DBNull.Value ? string.Empty : row["TJC_ELEMENT_CODE"].ToString(),
                TJC_DESC = row["TJC_DESC"] == DBNull.Value ? string.Empty : row["TJC_DESC"].ToString(),
                TJC_COST_AMT = row["TJC_COST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TJC_COST_AMT"].ToString()),
                TJC_MARGIN = row["TJC_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TJC_MARGIN"].ToString()),
                TJC_APP_1 = row["TJC_APP_1"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_APP_1"].ToString()),
                TJC_APP_2 = row["TJC_APP_2"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_APP_2"].ToString()),
                TJC_ACT = row["TJC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TJC_ACT"].ToString())
            };
        }
    } 

}
