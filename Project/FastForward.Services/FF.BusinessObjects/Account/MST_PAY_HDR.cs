using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class MST_PAY_HDR
    {
        public Int32 MPH_SEQ { get; set; }
        public String MPH_PAY_NO { get; set; }
        public DateTime MPH_PAY_DT { get; set; }
        public String MPH_COM { get; set; }
        public String MPH_OTHDOC_NO { get; set; }
        public String MPH_PAY_TP { get; set; }
        public String MPH_CREDITOR { get; set; }
        public String MPH_RMK { get; set; }
        public Decimal MPH_GROS_AMT { get; set; }
        public Decimal MPH_TAX { get; set; }
        public Decimal MPH_NET_AMT { get; set; }
        public Decimal MPH_ANAL1 { get; set; }
        public Decimal MPH_ANAL2 { get; set; }
        public String MPH_ANAL3 { get; set; }
        public String MPH_ANAL4 { get; set; }
        public String MPH_STUS { get; set; }
        public String MPH_CRE_BY { get; set; }
        public DateTime MPH_CRE_DT { get; set; }
        public String MPH_MOD_BY { get; set; }
        public DateTime MPH_MOD_DT { get; set; }
        public string MPH_SESSION_ID { get; set; }
        public static MST_PAY_HDR Converter(DataRow row)
        {
            return new MST_PAY_HDR
            {
                MPH_SEQ = row["MPH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPH_SEQ"].ToString()),
                MPH_PAY_NO = row["MPH_PAY_NO"] == DBNull.Value ? string.Empty : row["MPH_PAY_NO"].ToString(),
                MPH_PAY_DT = row["MPH_PAY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPH_PAY_DT"].ToString()),
                MPH_COM = row["MPH_COM"] == DBNull.Value ? string.Empty : row["MPH_COM"].ToString(),
                MPH_OTHDOC_NO = row["MPH_OTHDOC_NO"] == DBNull.Value ? string.Empty : row["MPH_OTHDOC_NO"].ToString(),
                MPH_PAY_TP = row["MPH_PAY_TP"] == DBNull.Value ? string.Empty : row["MPH_PAY_TP"].ToString(),
                MPH_CREDITOR = row["MPH_CREDITOR"] == DBNull.Value ? string.Empty : row["MPH_CREDITOR"].ToString(),
                MPH_RMK = row["MPH_RMK"] == DBNull.Value ? string.Empty : row["MPH_RMK"].ToString(),
                MPH_GROS_AMT = row["MPH_GROS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPH_GROS_AMT"].ToString()),
                MPH_TAX = row["MPH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPH_TAX"].ToString()),
                MPH_NET_AMT = row["MPH_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPH_NET_AMT"].ToString()),
                MPH_ANAL1 = row["MPH_ANAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPH_ANAL1"].ToString()),
                MPH_ANAL2 = row["MPH_ANAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPH_ANAL2"].ToString()),
                MPH_ANAL3 = row["MPH_ANAL3"] == DBNull.Value ? string.Empty : row["MPH_ANAL3"].ToString(),
                MPH_ANAL4 = row["MPH_ANAL4"] == DBNull.Value ? string.Empty : row["MPH_ANAL4"].ToString(),
                MPH_STUS = row["MPH_STUS"] == DBNull.Value ? string.Empty : row["MPH_STUS"].ToString(),
                MPH_CRE_BY = row["MPH_CRE_BY"] == DBNull.Value ? string.Empty : row["MPH_CRE_BY"].ToString(),
                MPH_CRE_DT = row["MPH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPH_CRE_DT"].ToString()),
                MPH_MOD_BY = row["MPH_MOD_BY"] == DBNull.Value ? string.Empty : row["MPH_MOD_BY"].ToString(),
                MPH_MOD_DT = row["MPH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPH_MOD_DT"].ToString())
            };
        }
    } 

}
