using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Account
{
    public class MST_ACC_TAX
    {
        public Int32 MAT_SEQ { get; set; }
        public String MAT_REQ_NO { get; set; }
        public Decimal MAT_TAX_AMT { get; set; }
        public Decimal MAT_TAX_RT { get; set; }
        public String MAT_ACC_NO { get; set; }
        public String MAT_ANAL1 { get; set; }
        public String MAT_ANAL2 { get; set; }
        public Decimal MAT_ANAL3 { get; set; }
        public Decimal MAT_ANAL4 { get; set; }
        public Int32 MAT_STUS { get; set; }
        public String MAT_CRE_BY { get; set; }
        public DateTime MAT_CRE_DT { get; set; }
        public String MAT_MOD_BY { get; set; }
        public DateTime MAT_MOD_DT { get; set; }
        public String MAT_COM { get; set; }
        public string MAT_TAX_CD { get; set; }
        public string MAT_SESSION_ID { get; set; }
        public Int32 UPDATED { get; set; }
        public Int32 NEW_ADDED { get; set; }
        public static MST_ACC_TAX Converter(DataRow row)
        {
            return new MST_ACC_TAX
            {
                MAT_SEQ = row["MAT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MAT_SEQ"].ToString()),
                MAT_REQ_NO = row["MAT_REQ_NO"] == DBNull.Value ? string.Empty : row["MAT_REQ_NO"].ToString(),
                MAT_TAX_AMT = row["MAT_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAT_TAX_AMT"].ToString()),
                MAT_TAX_RT = row["MAT_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAT_TAX_RT"].ToString()),
                MAT_ACC_NO = row["MAT_ACC_NO"] == DBNull.Value ? string.Empty : row["MAT_ACC_NO"].ToString(),
                MAT_ANAL1 = row["MAT_ANAL1"] == DBNull.Value ? string.Empty : row["MAT_ANAL1"].ToString(),
                MAT_ANAL2 = row["MAT_ANAL2"] == DBNull.Value ? string.Empty : row["MAT_ANAL2"].ToString(),
                MAT_ANAL3 = row["MAT_ANAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAT_ANAL3"].ToString()),
                MAT_ANAL4 = row["MAT_ANAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MAT_ANAL4"].ToString()),
                MAT_STUS = row["MAT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MAT_STUS"].ToString()),
                MAT_CRE_BY = row["MAT_CRE_BY"] == DBNull.Value ? string.Empty : row["MAT_CRE_BY"].ToString(),
                MAT_CRE_DT = row["MAT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MAT_CRE_DT"].ToString()),
                MAT_MOD_BY = row["MAT_MOD_BY"] == DBNull.Value ? string.Empty : row["MAT_MOD_BY"].ToString(),
                MAT_MOD_DT = row["MAT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MAT_MOD_DT"].ToString()),
                MAT_COM = row["MAT_COM"] == DBNull.Value ? string.Empty : row["MAT_COM"].ToString(),
                MAT_TAX_CD = row["MAT_TAX_CD"] == DBNull.Value ? string.Empty : row["MAT_TAX_CD"].ToString(),
                MAT_SESSION_ID = row["MAT_SESSION_ID"] == DBNull.Value ? string.Empty : row["MAT_SESSION_ID"].ToString()
            };
        }
    }
}
