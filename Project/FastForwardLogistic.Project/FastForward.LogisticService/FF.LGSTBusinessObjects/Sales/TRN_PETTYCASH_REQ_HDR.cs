using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class TRN_PETTYCASH_REQ_HDR
    {
        public Int32 TPRH_SEQ { get; set; }
        public String TPRH_REQ_NO { get; set; }
        public String TPRH_MANUAL_REF { get; set; }
        public DateTime TPRH_REQ_DT { get; set; }
        public String TPRH_REMARKS { get; set; }
        public String TPRH_STUS { get; set; }
        public Int32 TPRH_APP_1 { get; set; }
        public String TPRH_APP_1_BY { get; set; }
        public DateTime TPRH_APP_1_DT { get; set; }
        public Int32 TPRH_APP_2 { get; set; }
        public String TPRH_APP_2_BY { get; set; }
        public DateTime TPRH_APP_2_DT { get; set; }
        public Int32 TPRH_APP_3 { get; set; }
        public String TPRH_APP_3_BY { get; set; }
        public DateTime TPRH_APP_3_DT { get; set; }
        public String TPRH_CRE_BY { get; set; }
        public DateTime TPRH_CRE_DT { get; set; }
        public String TPRH_MOD_BY { get; set; }
        public DateTime TPRH_MOD_DT { get; set; }
        public Decimal TPRH_TOT_AMT { get; set; }
        public String TPRH_REQ_BY { get; set; }
        public String TPRH_SETTLE { get; set; }
        public String TPRH_TYPE { get; set; }
        public String TPRH_TYPE_DESC { get; set; }
        public String TPRH_PAY_TO { get; set; }
        public String TPRH_PAY_TO_NAME { get; set; }
        public String TPRH_PAY_TO_ADD1 { get; set; }
        public String TPRH_PAY_TO_ADD2 { get; set; }
        public Int32 TPRH_SUN_UPLOAD { get; set; }
        public String TPRH_PC_CD { get; set; }
        public Int32 TPRH_IS_PRINT { get; set; }
        public DateTime TPRH_PRINT_DT { get; set; }
        public DateTime TPRH_PAYMENT_DT { get; set; }
        public Int32 TPRH_PRO_APP1 { get; set; }
        public Int32 TPRH_PRO_APP2 { get; set; }
        public Int32 TPRH_PRO_APP3 { get; set; }
        public Int32 TPRH_IS_PROVISION { get; set; }
        public String TPRH_PRO_APP1_BY { get; set; }
        public DateTime TPRH_PRO_APP1_DT { get; set; }
        public String TPRH_PRO_APP2_BY { get; set; }
        public DateTime TPRH_PRO_APP2_DT { get; set; }
        public String TPRH_COM_CD { get; set; }
        public string TPRH_CRE_SESSION_ID { get; set; }
        public string TPRH_MOD_SESSION_ID { get; set; }
        public string TPRD_JOB_NO { get; set; }
        public static TRN_PETTYCASH_REQ_HDR Converter(DataRow row)
        {
            return new TRN_PETTYCASH_REQ_HDR
            {
                TPRH_SEQ = row["TPRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_SEQ"].ToString()),
                TPRH_REQ_NO = row["TPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRH_REQ_NO"].ToString(),
                TPRH_MANUAL_REF = row["TPRH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["TPRH_MANUAL_REF"].ToString(),
                TPRH_REQ_DT = row["TPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_REQ_DT"].ToString()),
                TPRH_REMARKS = row["TPRH_REMARKS"] == DBNull.Value ? string.Empty : row["TPRH_REMARKS"].ToString(),
                TPRH_STUS = row["TPRH_STUS"] == DBNull.Value ? string.Empty : row["TPRH_STUS"].ToString(),
                TPRH_APP_1 = row["TPRH_APP_1"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_APP_1"].ToString()),
                TPRH_APP_1_BY = row["TPRH_APP_1_BY"] == DBNull.Value ? string.Empty : row["TPRH_APP_1_BY"].ToString(),
                TPRH_APP_1_DT = row["TPRH_APP_1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_APP_1_DT"].ToString()),
                TPRH_APP_2 = row["TPRH_APP_2"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_APP_2"].ToString()),
                TPRH_APP_2_BY = row["TPRH_APP_2_BY"] == DBNull.Value ? string.Empty : row["TPRH_APP_2_BY"].ToString(),
                TPRH_APP_2_DT = row["TPRH_APP_2_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_APP_2_DT"].ToString()),
                TPRH_APP_3 = row["TPRH_APP_3"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_APP_3"].ToString()),
                TPRH_APP_3_BY = row["TPRH_APP_3_BY"] == DBNull.Value ? string.Empty : row["TPRH_APP_3_BY"].ToString(),
                TPRH_APP_3_DT = row["TPRH_APP_3_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_APP_3_DT"].ToString()),
                TPRH_CRE_BY = row["TPRH_CRE_BY"] == DBNull.Value ? string.Empty : row["TPRH_CRE_BY"].ToString(),
                TPRH_CRE_DT = row["TPRH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_CRE_DT"].ToString()),
                TPRH_MOD_BY = row["TPRH_MOD_BY"] == DBNull.Value ? string.Empty : row["TPRH_MOD_BY"].ToString(),
                TPRH_MOD_DT = row["TPRH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_MOD_DT"].ToString()),
                TPRH_TOT_AMT = row["TPRH_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRH_TOT_AMT"].ToString()),
                TPRH_REQ_BY = row["TPRH_REQ_BY"] == DBNull.Value ? string.Empty : row["TPRH_REQ_BY"].ToString(),
                TPRH_SETTLE = row["TPRH_SETTLE"] == DBNull.Value ? string.Empty : row["TPRH_SETTLE"].ToString(),
                TPRH_TYPE = row["TPRH_TYPE"] == DBNull.Value ? string.Empty : row["TPRH_TYPE"].ToString(),
                TPRH_PAY_TO = row["TPRH_PAY_TO"] == DBNull.Value ? string.Empty : row["TPRH_PAY_TO"].ToString(),
                TPRH_PAY_TO_NAME = row["TPRH_PAY_TO_NAME"] == DBNull.Value ? string.Empty : row["TPRH_PAY_TO_NAME"].ToString(),
                TPRH_PAY_TO_ADD1 = row["TPRH_PAY_TO_ADD1"] == DBNull.Value ? string.Empty : row["TPRH_PAY_TO_ADD1"].ToString(),
                TPRH_PAY_TO_ADD2 = row["TPRH_PAY_TO_ADD2"] == DBNull.Value ? string.Empty : row["TPRH_PAY_TO_ADD2"].ToString(),
                TPRH_SUN_UPLOAD = row["TPRH_SUN_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_SUN_UPLOAD"].ToString()),
                TPRH_PC_CD = row["TPRH_PC_CD"] == DBNull.Value ? string.Empty : row["TPRH_PC_CD"].ToString(),
                TPRH_IS_PRINT = row["TPRH_IS_PRINT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_IS_PRINT"].ToString()),
                TPRH_PRINT_DT = row["TPRH_PRINT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_PRINT_DT"].ToString()),
                TPRH_PAYMENT_DT = row["TPRH_PAYMENT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_PAYMENT_DT"].ToString()),
                TPRH_PRO_APP1 = row["TPRH_PRO_APP1"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_PRO_APP1"].ToString()),
                TPRH_PRO_APP2 = row["TPRH_PRO_APP2"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_PRO_APP2"].ToString()),
                TPRH_PRO_APP3 = row["TPRH_PRO_APP3"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_PRO_APP3"].ToString()),
                TPRH_IS_PROVISION = row["TPRH_IS_PROVISION"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_IS_PROVISION"].ToString()),
                TPRH_PRO_APP1_BY = row["TPRH_PRO_APP1_BY"] == DBNull.Value ? string.Empty : row["TPRH_PRO_APP1_BY"].ToString(),
                TPRH_PRO_APP1_DT = row["TPRH_PRO_APP1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_PRO_APP1_DT"].ToString()),
                TPRH_PRO_APP2_BY = row["TPRH_PRO_APP2_BY"] == DBNull.Value ? string.Empty : row["TPRH_PRO_APP2_BY"].ToString(),
                TPRH_PRO_APP2_DT = row["TPRH_PRO_APP2_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_PRO_APP2_DT"].ToString()),
                TPRH_COM_CD = row["TPRH_COM_CD"] == DBNull.Value ? string.Empty : row["TPRH_COM_CD"].ToString()
            };
        }

        public static TRN_PETTYCASH_REQ_HDR ConverterSub(DataRow row)
        {
            return new TRN_PETTYCASH_REQ_HDR
            {
                TPRH_SEQ = row["TPRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_SEQ"].ToString()),
                TPRH_REQ_NO = row["TPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRH_REQ_NO"].ToString(),
                TPRH_MANUAL_REF = row["TPRH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["TPRH_MANUAL_REF"].ToString(),
                TPRH_REQ_DT = row["TPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_REQ_DT"].ToString()),
            };
        }
        public static TRN_PETTYCASH_REQ_HDR ConverterEnquiry(DataRow row)
        {
            return new TRN_PETTYCASH_REQ_HDR
            {
                TPRH_REQ_NO = row["TPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRH_REQ_NO"].ToString(),
                TPRH_REQ_DT = row["TPRH_REQ_DT"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["TPRH_REQ_DT"].ToString()),
                TPRH_MANUAL_REF = row["TPRH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["TPRH_MANUAL_REF"].ToString(),
                TPRH_PC_CD = row["TPRH_PC_CD"] == DBNull.Value ? string.Empty : row["TPRH_PC_CD"].ToString(),
                TPRH_REQ_BY = row["TPRH_REQ_BY"] == DBNull.Value ? string.Empty : row["TPRH_REQ_BY"].ToString(),
                TPRD_JOB_NO = row["TPRD_JOB_NO"] == DBNull.Value ? string.Empty : row["TPRD_JOB_NO"].ToString(),
                TPRH_STUS = row["TPRH_STUS"] == DBNull.Value ? string.Empty : row["TPRH_STUS"].ToString(),
                TPRH_PAY_TO = row["TPRH_PAY_TO"] == DBNull.Value ? string.Empty : row["TPRH_PAY_TO"].ToString(),
                TPRH_SEQ = row["TPRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt16(row["TPRH_SEQ"].ToString()),
            };
        }

        public static TRN_PETTYCASH_REQ_HDR ConverterSubWithJobNo(DataRow row)
        {
            return new TRN_PETTYCASH_REQ_HDR
            {
                TPRH_SEQ = row["TPRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRH_SEQ"].ToString()),
                TPRH_REQ_NO = row["TPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRH_REQ_NO"].ToString(),
                TPRH_MANUAL_REF = row["TPRH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["TPRH_MANUAL_REF"].ToString(),
                TPRH_REQ_DT = row["TPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRH_REQ_DT"].ToString()),
                TPRD_JOB_NO = row["TPRD_JOB_NO"] == DBNull.Value ? string.Empty : row["TPRD_JOB_NO"].ToString(),
            };
        }

    } 
}
