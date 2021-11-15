using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class INR_RES
    {
        public Int32 IRS_SEQ { get; set; }
        public String IRS_COM { get; set; }
        public String IRS_CHNL { get; set; }
        public String IRS_RES_NO { get; set; }
        public DateTime IRS_RES_DT { get; set; }
        public String IRS_RES_TP { get; set; }
        public String IRS_CUST_TP { get; set; }
        public String IRS_CUST_CD { get; set; }
        public String IRS_STUS { get; set; }
        public String IRS_RMK { get; set; }
        public String IRS_ANAL_1 { get; set; }
        public String IRS_ANAL_2 { get; set; }
        public String IRS_ANAL_3 { get; set; }
        public String IRS_ANAL_4 { get; set; }
        public String IRS_ANAL_5 { get; set; }
        public String IRS_CRE_BY { get; set; }
        public DateTime IRS_CRE_DT { get; set; }
        public String IRS_CRE_SESSION { get; set; }
        public String IRS_CNL_BY { get; set; }
        public DateTime IRS_CNL_DT { get; set; }
        public String IRS_CNL_SESSION { get; set; }
        public String IRS_MOD_BY { get; set; }
        public DateTime IRS_MOD_DT { get; set; }
        public String IRS_MOD_SESSION { get; set; }
        public String MBE_NAME { get; set; }
        public String RRS_DESC { get; set; }
        public bool checkbaseitem { get; set; }
        public static INR_RES Converter(DataRow row)
        {
            return new INR_RES
            {
                IRS_SEQ = row["IRS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRS_SEQ"].ToString()),
                IRS_COM = row["IRS_COM"] == DBNull.Value ? string.Empty : row["IRS_COM"].ToString(),
                IRS_CHNL = row["IRS_CHNL"] == DBNull.Value ? string.Empty : row["IRS_CHNL"].ToString(),
                IRS_RES_NO = row["IRS_RES_NO"] == DBNull.Value ? string.Empty : row["IRS_RES_NO"].ToString(),
                IRS_RES_DT = row["IRS_RES_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_RES_DT"].ToString()),
                IRS_RES_TP = row["IRS_RES_TP"] == DBNull.Value ? string.Empty : row["IRS_RES_TP"].ToString(),
                IRS_CUST_TP = row["IRS_CUST_TP"] == DBNull.Value ? string.Empty : row["IRS_CUST_TP"].ToString(),
                IRS_CUST_CD = row["IRS_CUST_CD"] == DBNull.Value ? string.Empty : row["IRS_CUST_CD"].ToString(),
                IRS_STUS = row["IRS_STUS"] == DBNull.Value ? string.Empty : row["IRS_STUS"].ToString(),
                IRS_RMK = row["IRS_RMK"] == DBNull.Value ? string.Empty : row["IRS_RMK"].ToString(),
                IRS_ANAL_1 = row["IRS_ANAL_1"] == DBNull.Value ? string.Empty : row["IRS_ANAL_1"].ToString(),
                IRS_ANAL_2 = row["IRS_ANAL_2"] == DBNull.Value ? string.Empty : row["IRS_ANAL_2"].ToString(),
                IRS_ANAL_3 = row["IRS_ANAL_3"] == DBNull.Value ? string.Empty : row["IRS_ANAL_3"].ToString(),
                IRS_ANAL_4 = row["IRS_ANAL_4"] == DBNull.Value ? string.Empty : row["IRS_ANAL_4"].ToString(),
                IRS_ANAL_5 = row["IRS_ANAL_5"] == DBNull.Value ? string.Empty : row["IRS_ANAL_5"].ToString(),
                IRS_CRE_BY = row["IRS_CRE_BY"] == DBNull.Value ? string.Empty : row["IRS_CRE_BY"].ToString(),
                IRS_CRE_DT = row["IRS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_CRE_DT"].ToString()),
                IRS_CRE_SESSION = row["IRS_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IRS_CRE_SESSION"].ToString(),
                IRS_CNL_BY = row["IRS_CNL_BY"] == DBNull.Value ? string.Empty : row["IRS_CNL_BY"].ToString(),
                IRS_CNL_DT = row["IRS_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_CNL_DT"].ToString()),
                IRS_CNL_SESSION = row["IRS_CNL_SESSION"] == DBNull.Value ? string.Empty : row["IRS_CNL_SESSION"].ToString(),
                IRS_MOD_BY = row["IRS_MOD_BY"] == DBNull.Value ? string.Empty : row["IRS_MOD_BY"].ToString(),
                IRS_MOD_DT = row["IRS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_MOD_DT"].ToString()),
                IRS_MOD_SESSION = row["IRS_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IRS_MOD_SESSION"].ToString(),
                MBE_NAME = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                RRS_DESC = row["RRS_DESC"] == DBNull.Value ? string.Empty : row["RRS_DESC"].ToString()
            };
        }

        public static INR_RES ConverterNew(DataRow row)
        {
            return new INR_RES
            {
                IRS_SEQ = row["IRS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRS_SEQ"].ToString()),
                IRS_COM = row["IRS_COM"] == DBNull.Value ? string.Empty : row["IRS_COM"].ToString(),
                IRS_CHNL = row["IRS_CHNL"] == DBNull.Value ? string.Empty : row["IRS_CHNL"].ToString(),
                IRS_RES_NO = row["IRS_RES_NO"] == DBNull.Value ? string.Empty : row["IRS_RES_NO"].ToString(),
                IRS_RES_DT = row["IRS_RES_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_RES_DT"].ToString()),
                IRS_RES_TP = row["IRS_RES_TP"] == DBNull.Value ? string.Empty : row["IRS_RES_TP"].ToString(),
                IRS_CUST_TP = row["IRS_CUST_TP"] == DBNull.Value ? string.Empty : row["IRS_CUST_TP"].ToString(),
                IRS_CUST_CD = row["IRS_CUST_CD"] == DBNull.Value ? string.Empty : row["IRS_CUST_CD"].ToString(),
                IRS_STUS = row["IRS_STUS"] == DBNull.Value ? string.Empty : row["IRS_STUS"].ToString(),
                IRS_RMK = row["IRS_RMK"] == DBNull.Value ? string.Empty : row["IRS_RMK"].ToString(),
                IRS_ANAL_1 = row["IRS_ANAL_1"] == DBNull.Value ? string.Empty : row["IRS_ANAL_1"].ToString(),
                IRS_ANAL_2 = row["IRS_ANAL_2"] == DBNull.Value ? string.Empty : row["IRS_ANAL_2"].ToString(),
                IRS_ANAL_3 = row["IRS_ANAL_3"] == DBNull.Value ? string.Empty : row["IRS_ANAL_3"].ToString(),
                IRS_ANAL_4 = row["IRS_ANAL_4"] == DBNull.Value ? string.Empty : row["IRS_ANAL_4"].ToString(),
                IRS_ANAL_5 = row["IRS_ANAL_5"] == DBNull.Value ? string.Empty : row["IRS_ANAL_5"].ToString(),
                IRS_CRE_BY = row["IRS_CRE_BY"] == DBNull.Value ? string.Empty : row["IRS_CRE_BY"].ToString(),
                IRS_CRE_DT = row["IRS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_CRE_DT"].ToString()),
                IRS_CRE_SESSION = row["IRS_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IRS_CRE_SESSION"].ToString(),
                IRS_CNL_BY = row["IRS_CNL_BY"] == DBNull.Value ? string.Empty : row["IRS_CNL_BY"].ToString(),
                IRS_CNL_DT = row["IRS_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_CNL_DT"].ToString()),
                IRS_CNL_SESSION = row["IRS_CNL_SESSION"] == DBNull.Value ? string.Empty : row["IRS_CNL_SESSION"].ToString(),
                IRS_MOD_BY = row["IRS_MOD_BY"] == DBNull.Value ? string.Empty : row["IRS_MOD_BY"].ToString(),
                IRS_MOD_DT = row["IRS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRS_MOD_DT"].ToString()),
                IRS_MOD_SESSION = row["IRS_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IRS_MOD_SESSION"].ToString(),
            };
        }
    }
}
