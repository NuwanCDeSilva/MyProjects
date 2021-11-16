using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class TRN_PETTYCASH_SETTLE_HDR
    {
        public Int32 TPSH_SEQ_NO { get; set; }
        public String TPSH_SETTLE_NO { get; set; }
        public String TPSH_MAN_REF { get; set; }
        public DateTime TPSH_SETTLE_DT { get; set; }
        public String TPSH_REMARKS { get; set; }
        public Decimal TPSH_SETTLE_AMT { get; set; }
        public Int32 TPSH_SUN_UPLOAD { get; set; }
        public String TPSH_PC_CD { get; set; }
        public DateTime TPSH_PAY_DT { get; set; }
        public String TPSH_STUS { get; set; }
        public Int32 TPSH_APP1 { get; set; }
        public String TPSH_APP1_BY { get; set; }
        public DateTime TPSH_APP1_DT { get; set; }
        public Int32 TPSH_APP2 { get; set; }
        public String TPSH_APP2_BY { get; set; }
        public DateTime TPSH_APP2_DT { get; set; }
        public Int32 TPSH_APP3 { get; set; }
        public String TPSH_APP3_BY { get; set; }
        public DateTime TPSH_APP3_DT { get; set; }
        public Int32 TPSH_REJECT { get; set; }
        public String TPSH_REJ_BY { get; set; }
        public DateTime TPSH_REJ_DT { get; set; }
        public String TPSH_COM_CD { get; set; }
        public String TPSH_CRE_BY { get; set; }
        public DateTime TPSH_CRE_DT { get; set; }
        public String TPSH_MOD_BY { get; set; }
        public DateTime TPSH_MOD_DT { get; set; }
        public Int32 TPSH_CRE_SES_ID{get;set;}
        public Int32 TPSH_MOD_SES_ID { get; set; }
        public static TRN_PETTYCASH_SETTLE_HDR Converter(DataRow row)
        {
            return new TRN_PETTYCASH_SETTLE_HDR
            {
                TPSH_SEQ_NO = row["TPSH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_SEQ_NO"].ToString()),
                TPSH_SETTLE_NO = row["TPSH_SETTLE_NO"] == DBNull.Value ? string.Empty : row["TPSH_SETTLE_NO"].ToString(),
                TPSH_MAN_REF = row["TPSH_MAN_REF"] == DBNull.Value ? string.Empty : row["TPSH_MAN_REF"].ToString(),
                TPSH_SETTLE_DT = row["TPSH_SETTLE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_SETTLE_DT"].ToString()),
                TPSH_REMARKS = row["TPSH_REMARKS"] == DBNull.Value ? string.Empty : row["TPSH_REMARKS"].ToString(),
                TPSH_SETTLE_AMT = row["TPSH_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPSH_SETTLE_AMT"].ToString()),
                TPSH_SUN_UPLOAD = row["TPSH_SUN_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_SUN_UPLOAD"].ToString()),
                TPSH_PC_CD = row["TPSH_PC_CD"] == DBNull.Value ? string.Empty : row["TPSH_PC_CD"].ToString(),
                TPSH_PAY_DT = row["TPSH_PAY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_PAY_DT"].ToString()),
                TPSH_STUS = row["TPSH_STUS"] == DBNull.Value ? string.Empty : row["TPSH_STUS"].ToString(),
                TPSH_APP1 = row["TPSH_APP1"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_APP1"].ToString()),
                TPSH_APP1_BY = row["TPSH_APP1_BY"] == DBNull.Value ? string.Empty : row["TPSH_APP1_BY"].ToString(),
                TPSH_APP1_DT = row["TPSH_APP1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_APP1_DT"].ToString()),
                TPSH_APP2 = row["TPSH_APP2"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_APP2"].ToString()),
                TPSH_APP2_BY = row["TPSH_APP2_BY"] == DBNull.Value ? string.Empty : row["TPSH_APP2_BY"].ToString(),
                TPSH_APP2_DT = row["TPSH_APP2_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_APP2_DT"].ToString()),
                TPSH_APP3 = row["TPSH_APP3"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_APP3"].ToString()),
                TPSH_APP3_BY = row["TPSH_APP3_BY"] == DBNull.Value ? string.Empty : row["TPSH_APP3_BY"].ToString(),
                TPSH_APP3_DT = row["TPSH_APP3_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_APP3_DT"].ToString()),
                TPSH_REJECT = row["TPSH_REJECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_REJECT"].ToString()),
                TPSH_REJ_BY = row["TPSH_REJ_BY"] == DBNull.Value ? string.Empty : row["TPSH_REJ_BY"].ToString(),
                TPSH_REJ_DT = row["TPSH_REJ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_REJ_DT"].ToString()),
                TPSH_COM_CD = row["TPSH_COM_CD"] == DBNull.Value ? string.Empty : row["TPSH_COM_CD"].ToString(),
                TPSH_CRE_BY = row["TPSH_CRE_BY"] == DBNull.Value ? string.Empty : row["TPSH_CRE_BY"].ToString(),
                TPSH_CRE_DT = row["TPSH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_CRE_DT"].ToString()),
                TPSH_MOD_BY = row["TPSH_MOD_BY"] == DBNull.Value ? string.Empty : row["TPSH_MOD_BY"].ToString(),
                TPSH_MOD_DT = row["TPSH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_MOD_DT"].ToString()),
                TPSH_CRE_SES_ID = row["TPSH_CRE_SES_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_CRE_SES_ID"].ToString()),
                TPSH_MOD_SES_ID = row["TPSH_MOD_SES_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSH_MOD_SES_ID"].ToString())
            };
        }
    } 

}
