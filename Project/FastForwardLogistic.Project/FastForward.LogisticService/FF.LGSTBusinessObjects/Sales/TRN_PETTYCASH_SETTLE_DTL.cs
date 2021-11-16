using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class TRN_PETTYCASH_SETTLE_DTL
    {
        public Int32 TPSD_SEQ_NO { get; set; }
        public String TPSD_SETTLE_NO { get; set; }
        public Int32 TPSD_LINE_NO { get; set; }
        public String TPSD_JOB_NO { get; set; }
        public String TPSD_ELEMENT_CD { get; set; }
        public String TPSD_ELEMENT_DESC { get; set; }
        public Decimal TPSD_REQ_AMT { get; set; }
        public Decimal TPSD_SETTLE_AMT { get; set; }
        public String TPSD_CRE_BY { get; set; }
        public DateTime TPSD_CRE_DT { get; set; }
        public Int32 TPSD_ATT_RECEIPT { get; set; }
        public String TPSD_REQ_NO { get; set; }
        public String TPSD_REMARKS { get; set; }
        public String TPSD_VEC_TELE { get; set; }
        public Int32 TPSD_ACT { get; set; }
        public Int32 TPSD_SETLE_LINO_NO { get; set; }
        public String TPSD_RECEIPT_NO { get; set; }
        public Int32 TPSD_SET_LINE { get; set; }
        public static TRN_PETTYCASH_SETTLE_DTL Converter(DataRow row)
        {
            return new TRN_PETTYCASH_SETTLE_DTL
            {
                TPSD_SEQ_NO = row["TPSD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSD_SEQ_NO"].ToString()),
                TPSD_SETTLE_NO = row["TPSD_SETTLE_NO"] == DBNull.Value ? string.Empty : row["TPSD_SETTLE_NO"].ToString(),
                TPSD_LINE_NO = row["TPSD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSD_LINE_NO"].ToString()),
                TPSD_JOB_NO = row["TPSD_JOB_NO"] == DBNull.Value ? string.Empty : row["TPSD_JOB_NO"].ToString(),
                TPSD_ELEMENT_CD = row["TPSD_ELEMENT_CD"] == DBNull.Value ? string.Empty : row["TPSD_ELEMENT_CD"].ToString(),
                TPSD_ELEMENT_DESC = row["TPSD_ELEMENT_DESC"] == DBNull.Value ? string.Empty : row["TPSD_ELEMENT_DESC"].ToString(),
                TPSD_REQ_AMT = row["TPSD_REQ_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPSD_REQ_AMT"].ToString()),
                TPSD_SETTLE_AMT = row["TPSD_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPSD_SETTLE_AMT"].ToString()),
                TPSD_CRE_BY = row["TPSD_CRE_BY"] == DBNull.Value ? string.Empty : row["TPSD_CRE_BY"].ToString(),
                TPSD_CRE_DT = row["TPSD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSD_CRE_DT"].ToString()),
                TPSD_ATT_RECEIPT = row["TPSD_ATT_RECEIPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSD_ATT_RECEIPT"].ToString()),
                TPSD_REQ_NO = row["TPSD_REQ_NO"] == DBNull.Value ? string.Empty : row["TPSD_REQ_NO"].ToString(),
                TPSD_REMARKS = row["TPSD_REMARKS"] == DBNull.Value ? string.Empty : row["TPSD_REMARKS"].ToString(),
                TPSD_VEC_TELE = row["TPSD_VEC_TELE"] == DBNull.Value ? string.Empty : row["TPSD_VEC_TELE"].ToString(),
                TPSD_ACT = row["TPSD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSD_ACT"].ToString()),
                TPSD_SETLE_LINO_NO = row["TPSD_SETLE_LINO_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPSD_SETLE_LINO_NO"].ToString())
            };
        }
    } 

}
