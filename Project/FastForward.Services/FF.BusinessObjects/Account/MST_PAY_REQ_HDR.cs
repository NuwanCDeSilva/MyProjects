using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class MST_PAY_REQ_HDR
    {
        public Int32 MPRH_SEQ { get; set; }
        public String MPRH_REQ_NO { get; set; }
        public DateTime MPRH_REQ_DT { get; set; }
        public String MPRH_PAY_TP { get; set; }
        public String MPRH_CREDITOR { get; set; }
        public String MPRH_RMK { get; set; }
        public Decimal MPRH_GROS_AMT { get; set; }
        public Decimal MPRH_TAX { get; set; }
        public Decimal MPRH_NET_AMT { get; set; }
        public Int32 MPRH_ANAL1 { get; set; }
        public Decimal MPRH_ANAL2 { get; set; }
        public String MPRH_ANAL3 { get; set; }
        public String MPRH_ANAL4 { get; set; }
        public String MPRH_STUS { get; set; }
        public String MPRH_CRE_BY { get; set; }
        public DateTime MPRH_CRE_DT { get; set; }
        public String MPRH_MOD_BY { get; set; }
        public DateTime MPRH_MOD_DT { get; set; }
        public string MPRH_COM { get; set; }
        public string MPRH_SESSION_ID { get;set; }
        public string MPRH_REQ_TP { get; set; }
        public string MPRH_REF_NO { get; set; }
        public static MST_PAY_REQ_HDR Converter(DataRow row)
        {
            return new MST_PAY_REQ_HDR
            {
                MPRH_SEQ = row["MPRH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRH_SEQ"].ToString()),
                MPRH_REQ_NO = row["MPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["MPRH_REQ_NO"].ToString(),
                MPRH_REQ_DT = row["MPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRH_REQ_DT"].ToString()),
                MPRH_PAY_TP = row["MPRH_PAY_TP"] == DBNull.Value ? string.Empty : row["MPRH_PAY_TP"].ToString(),
                MPRH_CREDITOR = row["MPRH_CREDITOR"] == DBNull.Value ? string.Empty : row["MPRH_CREDITOR"].ToString(),
                MPRH_RMK = row["MPRH_RMK"] == DBNull.Value ? string.Empty : row["MPRH_RMK"].ToString(),
                MPRH_GROS_AMT = row["MPRH_GROS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRH_GROS_AMT"].ToString()),
                MPRH_TAX = row["MPRH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRH_TAX"].ToString()),
                MPRH_NET_AMT = row["MPRH_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRH_NET_AMT"].ToString()),
                MPRH_ANAL1 = row["MPRH_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRH_ANAL1"].ToString()),
                MPRH_ANAL2 = row["MPRH_ANAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRH_ANAL2"].ToString()),
                MPRH_ANAL3 = row["MPRH_ANAL3"] == DBNull.Value ? string.Empty : row["MPRH_ANAL3"].ToString(),
                MPRH_ANAL4 = row["MPRH_ANAL4"] == DBNull.Value ? string.Empty : row["MPRH_ANAL4"].ToString(),
                MPRH_STUS = row["MPRH_STUS"] == DBNull.Value ? string.Empty : row["MPRH_STUS"].ToString(),
                MPRH_CRE_BY = row["MPRH_CRE_BY"] == DBNull.Value ? string.Empty : row["MPRH_CRE_BY"].ToString(),
                MPRH_CRE_DT = row["MPRH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRH_CRE_DT"].ToString()),
                MPRH_MOD_BY = row["MPRH_MOD_BY"] == DBNull.Value ? string.Empty : row["MPRH_MOD_BY"].ToString(),
                MPRH_MOD_DT = row["MPRH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRH_MOD_DT"].ToString()),
                MPRH_COM = row["MPRH_COM"] == DBNull.Value ? string.Empty : row["MPRH_COM"].ToString(),
                MPRH_REQ_TP = row["MPRH_REQ_TP"] == DBNull.Value ? string.Empty : row["MPRH_REQ_TP"].ToString(),
                MPRH_REF_NO = row["MPRH_REF_NO"] == DBNull.Value ? string.Empty : row["MPRH_REF_NO"].ToString()

            };
        }
    } 

}
