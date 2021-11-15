using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Financial
{
    public class Ref_Bill_Collet
    {
        public Int32 RBC_SEQ { get; set; }
        public String RBC_COM { get; set; }
        public DateTime RBC_DT { get; set; }
        public String RBC_COL_LOC { get; set; }
        public String RBC_M_LOC { get; set; }
        public Decimal RBC_BF_DUE { get; set; }
        public Decimal RBC_GR_COL { get; set; }
        public Decimal RBC_CAN_CRE { get; set; }
        public Decimal RBC_TOBE_HDOV { get; set; }
        public Decimal RBC_1HDOV { get; set; }
        public DateTime RBC_1HDOV_DT { get; set; }
        public Decimal RBC_2HDOV { get; set; }
        public DateTime RBC_2HDOV_DT { get; set; }
        public Decimal RBC_3HDOV { get; set; }
        public DateTime RBC_3HDOV_DT { get; set; }
        public Decimal RBC_ACTHDOV { get; set; }
        public Decimal RBC_DIFF { get; set; }
        public Decimal RBC_BF_BAL { get; set; }
        public Decimal RBC_BURG_SHRT { get; set; }
        public Decimal RBC_ACT_SHRT { get; set; }
        public Decimal RBC_SYSISS_SHRT { get; set; }
        public Decimal RBC_SLIPON_SHRT { get; set; }
        public Decimal RBC_PRESETT_SHRT { get; set; }
        public Decimal RBC_CAN_SETTL { get; set; }
        public Decimal RBC_ACT_EXCESS { get; set; }
        public Decimal RBC_SYSISS_EXCESS { get; set; }
        public Decimal RBC_SLIPON_EXCESS { get; set; }
        public Decimal RBC_OTH_SHRT { get; set; }
        public Decimal RBC_OTH_EXCESS { get; set; }
        public Decimal RBC_TOT_DIFF { get; set; }
        public Decimal RBC_CAN_CRE_1 { get; set; }
        public Int32 RBC_CAN_ACT { get; set; }///////////////////////////////////
        public String RBC_CRE_BY { get; set; }
        public DateTime RBC_CRE_DT { get; set; }
        public Int32 RBC_UPD_STUS { get; set; }
        public DateTime RBC_UPD_DT { get; set; }
        public Int32 RBC_REC_STUS { get; set; }
        public Int32 RBC_CONF_STUS { get; set; }




        public static Ref_Bill_Collet Converter(DataRow row)
        {
            return new Ref_Bill_Collet
            {
                RBC_SEQ = row["RBC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBC_SEQ"].ToString()),
                RBC_COM = row["RBC_COM"] == DBNull.Value ? string.Empty : row["RBC_COM"].ToString(),
                RBC_DT = row["RBC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_DT"].ToString()),
                RBC_COL_LOC = row["RBC_COL_LOC"] == DBNull.Value ? string.Empty : row["RBC_COL_LOC"].ToString(),
                RBC_M_LOC = row["RBC_M_LOC"] == DBNull.Value ? string.Empty : row["RBC_M_LOC"].ToString(),

                RBC_BF_DUE = row["RBC_BF_DUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_BF_DUE"].ToString()),
                RBC_GR_COL = row["RBC_GR_COL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_GR_COL"].ToString()),
                RBC_CAN_CRE = row["RBC_CAN_CRE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_CAN_CRE"].ToString()),
                RBC_TOBE_HDOV = row["RBC_TOBE_HDOV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_TOBE_HDOV"].ToString()),

                RBC_1HDOV = row["RBC_1HDOV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_1HDOV"].ToString()),
                RBC_1HDOV_DT = row["RBC_1HDOV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_1HDOV_DT"].ToString()),

                RBC_2HDOV = row["RBC_2HDOV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_2HDOV"].ToString()),
                RBC_2HDOV_DT = row["RBC_2HDOV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_2HDOV_DT"].ToString()),

                RBC_3HDOV = row["RBC_3HDOV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_3HDOV"].ToString()),
                RBC_3HDOV_DT = row["RBC_3HDOV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_3HDOV_DT"].ToString()),

                RBC_ACTHDOV = row["RBC_ACTHDOV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_ACTHDOV"].ToString()),
                RBC_DIFF = row["RBC_DIFF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_DIFF"].ToString()),
                RBC_BF_BAL = row["RBC_BF_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_BF_BAL"].ToString()),
                RBC_BURG_SHRT = row["RBC_BURG_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_BURG_SHRT"].ToString()),
                RBC_ACT_SHRT = row["RBC_ACT_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_ACT_SHRT"].ToString()),
                RBC_SYSISS_SHRT = row["RBC_SYSISS_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_SYSISS_SHRT"].ToString()),
                RBC_SLIPON_SHRT = row["RBC_SLIPON_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_SLIPON_SHRT"].ToString()),
                RBC_PRESETT_SHRT = row["RBC_PRESETT_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_PRESETT_SHRT"].ToString()),
                RBC_CAN_SETTL = row["RBC_CAN_SETTL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_CAN_SETTL"].ToString()),
                RBC_ACT_EXCESS = row["RBC_ACT_EXCESS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_ACT_EXCESS"].ToString()),
                RBC_SYSISS_EXCESS = row["RBC_SYSISS_EXCESS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_SYSISS_EXCESS"].ToString()),
                RBC_SLIPON_EXCESS = row["RBC_SLIPON_EXCESS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_SLIPON_EXCESS"].ToString()),
                RBC_OTH_SHRT = row["RBC_OTH_SHRT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_OTH_SHRT"].ToString()),
                RBC_OTH_EXCESS = row["RBC_OTH_EXCESS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_OTH_EXCESS"].ToString()),
                RBC_TOT_DIFF = row["RBC_TOT_DIFF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_TOT_DIFF"].ToString()),
                RBC_CAN_CRE_1 = row["RBC_CAN_CRE_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RBC_CAN_CRE_1"].ToString()),

                RBC_CAN_ACT = row["RBC_CAN_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBC_CAN_ACT"].ToString()),
                RBC_CRE_BY = row["RBC_CRE_BY"] == DBNull.Value ? string.Empty : row["RBC_CRE_BY"].ToString(),
                RBC_CRE_DT = row["RBC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_CRE_DT"].ToString()),
                RBC_UPD_STUS = row["RBC_UPD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBC_UPD_STUS"].ToString()),
                RBC_UPD_DT = row["RBC_UPD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RBC_UPD_DT"].ToString()),
                RBC_REC_STUS = row["RBC_REC_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBC_REC_STUS"].ToString()),
                RBC_CONF_STUS = row["RBC_CONF_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RBC_CONF_STUS"].ToString())
            };
        }
    }
}
