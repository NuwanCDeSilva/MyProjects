using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class SAR_PAY_TP
    {
        public String SAPT_DESC { get; set; }
        public String SAPT_ACT { get; set; }
        public DateTime SAPT_CRE_WHEN { get; set; }
        public String SAPT_CRE_BY { get; set; }
        public DateTime SAPT_MOD_WHEN { get; set; }
        public String SAPT_MOD_BY { get; set; }
        public Int32 SAPT_IS_SETTLE_BANK { get; set; }
        public String SAPT_CD { get; set; }
        public Int32 SAPT_NEED_UPLOAD_SUN { get; set; }
        public String SAPT_SUN_ACC { get; set; }
        public String SAPT_SUN_ACC_TYPE { get; set; }
        public Int32 SAPT_SEQ { get; set; }
        public Int32 SAPT_REM_APP { get; set; }
        public static SAR_PAY_TP Converter(DataRow row)
        {
            return new SAR_PAY_TP
            {
                SAPT_DESC = row["SAPT_DESC"] == DBNull.Value ? string.Empty : row["SAPT_DESC"].ToString(),
                SAPT_ACT = row["SAPT_ACT"] == DBNull.Value ? string.Empty : row["SAPT_ACT"].ToString(),
                SAPT_CRE_WHEN = row["SAPT_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPT_CRE_WHEN"].ToString()),
                SAPT_CRE_BY = row["SAPT_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPT_CRE_BY"].ToString(),
                SAPT_MOD_WHEN = row["SAPT_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPT_MOD_WHEN"].ToString()),
                SAPT_MOD_BY = row["SAPT_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPT_MOD_BY"].ToString(),
                SAPT_IS_SETTLE_BANK = row["SAPT_IS_SETTLE_BANK"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPT_IS_SETTLE_BANK"].ToString()),
                SAPT_CD = row["SAPT_CD"] == DBNull.Value ? string.Empty : row["SAPT_CD"].ToString(),
                SAPT_NEED_UPLOAD_SUN = row["SAPT_NEED_UPLOAD_SUN"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPT_NEED_UPLOAD_SUN"].ToString()),
                SAPT_SUN_ACC = row["SAPT_SUN_ACC"] == DBNull.Value ? string.Empty : row["SAPT_SUN_ACC"].ToString(),
                SAPT_SUN_ACC_TYPE = row["SAPT_SUN_ACC_TYPE"] == DBNull.Value ? string.Empty : row["SAPT_SUN_ACC_TYPE"].ToString(),
                SAPT_SEQ = row["SAPT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPT_SEQ"].ToString()),
                SAPT_REM_APP = row["SAPT_REM_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPT_REM_APP"].ToString())
            };
        }
    } 

}
