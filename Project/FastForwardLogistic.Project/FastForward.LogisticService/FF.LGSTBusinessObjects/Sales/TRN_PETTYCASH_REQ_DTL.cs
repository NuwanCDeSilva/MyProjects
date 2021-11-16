using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    [Serializable]
    public class TRN_PETTYCASH_REQ_DTL
    {
        public Int32 TPRD_SEQ { get; set; }
        public String TPRD_REQ_NO { get; set; }
        public Int32 TPRD_LINE_NO { get; set; }
        public String TPRD_ELEMENT_CD { get; set; }
        public Decimal TPRD_ELEMENT_AMT { get; set; }
        public String TPRD_CRE_BY { get; set; }
        public DateTime TPRD_CRE_DT { get; set; }
        public String TPRD_MOD_BY { get; set; }
        public DateTime TPRD_MOD_DT { get; set; }
        public String TPRD_ELEMENT_DESC { get; set; }
        public String TPRD_CURRENCY_CODE { get; set; }
        public String TPRD_JOB_NO { get; set; }
        public Decimal TPRD_BALANCE_SET { get; set; }
        public String TPRD_COMMENTS { get; set; }
        public String TPRD_UOM { get; set; }
        public Decimal TPRD_NO_UNITS { get; set; }
        public Decimal TPRD_UNIT_PRICE { get; set; }
        public Decimal TPRD_EX_RATE { get; set; }
        public Decimal TPRD_PRO_ELEMENT_AMT { get; set; }
        public Decimal TPRD_PRO_UNITS { get; set; }
        public Decimal TPRD_PRO_UNIT_PRICE { get; set; }
        public String TPRD_PRO_CURRENCY { get; set; }
        public Decimal TPRD_PRO_EX_RATE { get; set; }
        public Int32 TPRD_IS_ACTUAL { get; set; }
        public DateTime TPRD_UPLOAD_DATE { get; set; }
        public Int32 TPRD_SUN_UPLOAD { get; set; }
        public string TPRD_VEC_TELE { get; set; }
        public string TPRD_INV_NO { get; set; }
        public Int32 TPRD_ACT { get; set; }
        public string TPRD_INV_DT { get; set; }
        public static TRN_PETTYCASH_REQ_DTL Converter(DataRow row)
        {
            TRN_PETTYCASH_REQ_DTL _obj = new TRN_PETTYCASH_REQ_DTL();
            //return new TRN_PETTYCASH_REQ_DTL
            //{
                _obj.TPRD_SEQ = row["TPRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRD_SEQ"].ToString());
                _obj.TPRD_REQ_NO = row["TPRD_REQ_NO"] == DBNull.Value ? string.Empty : row["TPRD_REQ_NO"].ToString();
                _obj.TPRD_LINE_NO = row["TPRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRD_LINE_NO"].ToString());
                _obj.TPRD_ELEMENT_CD = row["TPRD_ELEMENT_CD"] == DBNull.Value ? string.Empty : row["TPRD_ELEMENT_CD"].ToString();
                _obj.TPRD_ELEMENT_AMT = row["TPRD_ELEMENT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_ELEMENT_AMT"].ToString());
                _obj.TPRD_CRE_BY = row["TPRD_CRE_BY"] == DBNull.Value ? string.Empty : row["TPRD_CRE_BY"].ToString();
                _obj.TPRD_CRE_DT = row["TPRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRD_CRE_DT"].ToString());
                _obj.TPRD_MOD_BY = row["TPRD_MOD_BY"] == DBNull.Value ? string.Empty : row["TPRD_MOD_BY"].ToString();
                _obj.TPRD_MOD_DT = row["TPRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRD_MOD_DT"].ToString());
                _obj.TPRD_ELEMENT_DESC = row["TPRD_ELEMENT_DESC"] == DBNull.Value ? string.Empty : row["TPRD_ELEMENT_DESC"].ToString();
                _obj.TPRD_CURRENCY_CODE = row["TPRD_CURRENCY_CODE"] == DBNull.Value ? string.Empty : row["TPRD_CURRENCY_CODE"].ToString();
                _obj.TPRD_JOB_NO = row["TPRD_JOB_NO"] == DBNull.Value ? string.Empty : row["TPRD_JOB_NO"].ToString();
                _obj.TPRD_BALANCE_SET = row["TPRD_BALANCE_SET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_BALANCE_SET"].ToString());
                _obj.TPRD_COMMENTS = row["TPRD_COMMENTS"] == DBNull.Value ? string.Empty : row["TPRD_COMMENTS"].ToString();
                _obj.TPRD_UOM = row["TPRD_UOM"] == DBNull.Value ? string.Empty : row["TPRD_UOM"].ToString();
                _obj.TPRD_NO_UNITS = row["TPRD_NO_UNITS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_NO_UNITS"].ToString());
                _obj.TPRD_UNIT_PRICE = row["TPRD_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_UNIT_PRICE"].ToString());
                _obj.TPRD_EX_RATE = row["TPRD_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_EX_RATE"].ToString());
                _obj.TPRD_PRO_ELEMENT_AMT = row["TPRD_PRO_ELEMENT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_PRO_ELEMENT_AMT"].ToString());
                _obj.TPRD_PRO_UNITS = row["TPRD_PRO_UNITS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_PRO_UNITS"].ToString());
                _obj.TPRD_PRO_UNIT_PRICE = row["TPRD_PRO_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_PRO_UNIT_PRICE"].ToString());
                _obj.TPRD_PRO_CURRENCY = row["TPRD_PRO_CURRENCY"] == DBNull.Value ? string.Empty : row["TPRD_PRO_CURRENCY"].ToString();
                _obj.TPRD_PRO_EX_RATE = row["TPRD_PRO_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TPRD_PRO_EX_RATE"].ToString());
                _obj.TPRD_IS_ACTUAL = row["TPRD_IS_ACTUAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRD_IS_ACTUAL"].ToString());
                _obj.TPRD_UPLOAD_DATE = row["TPRD_UPLOAD_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPRD_UPLOAD_DATE"].ToString());
                _obj.TPRD_SUN_UPLOAD = row["TPRD_SUN_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRD_SUN_UPLOAD"].ToString());
                _obj.TPRD_VEC_TELE = row["TPRD_VEC_TELE"] == DBNull.Value ? string.Empty : row["TPRD_VEC_TELE"].ToString();
                _obj.TPRD_INV_NO = row["TPRD_INV_NO"] == DBNull.Value ? string.Empty : row["TPRD_INV_NO"].ToString();
                _obj.TPRD_INV_DT = row["TPRD_INV_DT"] == DBNull.Value ? string.Empty : row["TPRD_INV_DT"].ToString();
                _obj.TPRD_ACT = row["TPRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TPRD_ACT"].ToString());
            //};
            return _obj;
        }
    } 

}
